using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Adventure.Net;

public class SaveObjectConverter : JsonConverter<SaveObject>
{
    public override SaveObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new Exception("Deserialize directly to SaveGame");
    }

    public static Object Restore(SaveObject w)
    {
        var obj = Objects.All.FirstOrDefault(x => x.GetHashCode() == w.T);

        if (obj == null)
        {
            throw new ArgumentException($"Object with type {w.T} not found.");
        }

        // clear current parent/children
        obj.Parent = null;
        obj.Children = [];

        // update parent/children from save
        if (w.P > 0)
        {
            obj.Parent = Objects.All.Single(x => x.Id == w.P);
        }

        if (w.C.Count > 0)
        {
            obj.Children = Objects.All.Where(x => w.C.Contains(x.Id)).ToList();
        }

        SetBools(w, obj);

        SetNumbers(w, obj);
        
        SetStrings(w, obj);

        SetObjects(w, obj);

        return obj;
    }

    public override void Write(Utf8JsonWriter writer, SaveObject wrapper, JsonSerializerOptions options)
    {
        var obj = wrapper.Object;

        writer.WriteStartObject();

        writer.WriteNumber("id", obj.Id);
        writer.WriteNumber("t", obj.GetHashCode());

        if (obj.Parent != null)
        {
            writer.WriteNumber("p", obj.Parent.Id);
        }

        WriteChildren(writer, obj);

        WriteAttributes(writer, obj);

        WriteNumbers(writer, obj);

        WriteStrings(writer, obj);
        
        WriteObjects(writer, obj);

        writer.WriteEndObject();
    }

    private static void WriteObjects(Utf8JsonWriter writer, Object obj)
    {
        var objProps = GetObjectProps(obj);

        var objects = objProps.Select(p => (Object)p.GetValue(obj)).ToList();

        if (objects.Count > 0)
        {
            writer.WriteStartArray("o");

            foreach (var o in objects)
            {
                JsonSerializer.Serialize(writer, o?.Id ?? -1);
            }

            writer.WriteEndArray();
        }
    }

    private static void WriteStrings(Utf8JsonWriter writer, Object obj)
    {
        var stringProps = GetStringProps(obj);

        var strings = stringProps.Select(p => (string)p.GetValue(obj)).ToList();

        if (strings.Count > 0)
        {
            writer.WriteStartArray("s");

            foreach (var s in strings)
            {
                JsonSerializer.Serialize(writer, s);
            }

            writer.WriteEndArray();
        }
    }

    private static void WriteNumbers(Utf8JsonWriter writer, Object obj)
    {
        var numericProps = GetNumericProps(obj);

        var numbers = numericProps.Select(p => Convert.ToDouble(p.GetValue(obj))).ToList();

        if (numbers.Count > 0)
        {
            writer.WriteStartArray("n");

            foreach (var n in numbers)
            {
                JsonSerializer.Serialize(writer, n);
            }

            writer.WriteEndArray();
        }
    }

    private static void WriteChildren(Utf8JsonWriter writer, Object obj)
    {
        if (obj.Children.Count > 0)
        {
            var children = obj.Children.Select(x => x.Id);

            writer.WriteStartArray("c");

            foreach (var child in children)
            {
                JsonSerializer.Serialize(writer, child);
            }

            writer.WriteEndArray();
        }
    }

    private static void WriteAttributes(Utf8JsonWriter writer, Object obj)
    {
        var attributes = GetAttributes(obj, out int bitCount);
        writer.WriteBase64String("a", attributes);
        writer.WriteNumber("ax", bitCount);
    }

    private static byte[] GetAttributes(Object obj, out int bitCount)
    {
        var boolProps = GetBoolProps(obj);

        var boolValues = new List<bool>();

        foreach (var prop in boolProps)
        {
            boolValues.Add((bool)prop.GetValue(obj));
        }

        bitCount = boolValues.Count;

        return GetBytes(boolValues);
    }

    private static bool ReadWriteProps(PropertyInfo prop) => prop.CanRead && prop.CanWrite && prop.GetCustomAttribute<JsonIgnoreAttribute>() == null;

    private static IOrderedEnumerable<PropertyInfo> GetBoolProps(Object obj)
    {
        return obj.GetType().GetProperties()
            .Where(prop => prop.PropertyType == typeof(bool) && ReadWriteProps(prop))
            .OrderBy(prop => prop.Name);
    }

    private static IOrderedEnumerable<PropertyInfo> GetNumericProps(Object obj)
    {
        return obj.GetType().GetProperties()
            .Where(prop => IsNumericType(prop.PropertyType) && prop.Name != "Id" && ReadWriteProps(prop))
            .OrderBy(prop => prop.Name);
    }

    private static IOrderedEnumerable<PropertyInfo> GetObjectProps(Object obj)
    {
        return obj.GetType().GetProperties()
            .Where(prop => prop.PropertyType.IsSubclassOf(typeof(Object)) && ReadWriteProps(prop))
            .OrderBy(prop => prop.Name);
    }

    private static IOrderedEnumerable<PropertyInfo> GetStringProps(Object obj)
    {
        return obj.GetType().GetProperties()
            .Where(prop => prop.PropertyType == typeof(string) && ReadWriteProps(prop))
            .OrderBy(prop => prop.Name);
    }

    private static byte[] GetBytes(IList<bool> attributes)
    {
        int byteCount = (attributes.Count + 7) / 8;
        byte[] bytes = new byte[byteCount];

        for (int i = 0; i < attributes.Count; i++)
        {
            if (attributes[i])
            {
                bytes[i / 8] |= (byte)(1 << (i % 8));
            }
        }

        return bytes;
    }

    public static List<bool> GetBoolValues(byte[] bytes, int bitCount)
    {
        List<bool> attributes = new List<bool>(bitCount);

        for (int i = 0; i < bitCount; i++)
        {
            int byteIndex = i / 8;
            int bitIndex = i % 8;
            bool value = (bytes[byteIndex] & (1 << bitIndex)) != 0;
            attributes.Add(value);
        }

        return attributes;
    }

    private static bool IsNumericType(Type type)
    {
        switch (Type.GetTypeCode(type))
        {
            case TypeCode.Byte:
            case TypeCode.SByte:
            case TypeCode.UInt16:
            case TypeCode.UInt32:
            case TypeCode.UInt64:
            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
            case TypeCode.Decimal:
            case TypeCode.Double:
            case TypeCode.Single:
                return true;
            default:
                return false;
        }
    }

    private static void SetBools(SaveObject w, Object obj)
    {
        var boolProps = GetBoolProps(obj);
        byte[] bytes = Convert.FromBase64String(w.A);
        var bools = GetBoolValues(bytes, w.AX);

        foreach (var boolProp in boolProps)
        {
            boolProp.SetValue(obj, bools[0]);
            bools.RemoveAt(0);
        }
    }

    private static void SetNumbers(SaveObject w, Object obj)
    {
        var numberProps = GetNumericProps(obj);
        var n = w.N;

        foreach (var numberProp in numberProps)
        {
            SetNumericValue(obj, numberProp, n[0]);
            n.RemoveAt(0);
        }
    }

    private static void SetNumericValue(Object obj, PropertyInfo prop, double v)
    {
        switch (Type.GetTypeCode(prop.PropertyType))
        {
            case TypeCode.Byte:
                prop.SetValue(obj, Convert.ToByte(v));
                break;
            case TypeCode.SByte:
                prop.SetValue(obj, Convert.ToSByte(v));
                break;
            case TypeCode.UInt16:
                prop.SetValue(obj, Convert.ToUInt16(v));
                break;
            case TypeCode.UInt32:
                prop.SetValue(obj, Convert.ToUInt32(v));
                break;
            case TypeCode.UInt64:
                prop.SetValue(obj, Convert.ToUInt64(v));
                break;
            case TypeCode.Int16:
                prop.SetValue(obj, Convert.ToInt16(v));
                break;
            case TypeCode.Int32:
                prop.SetValue(obj, Convert.ToInt32(v));
                break;
            case TypeCode.Int64:
                prop.SetValue(obj, Convert.ToInt64(v));
                break;
            case TypeCode.Decimal:
                prop.SetValue(obj, Convert.ToDecimal(v));
                break;
            case TypeCode.Double:
                prop.SetValue(obj, Convert.ToDouble(v));
                break;
            case TypeCode.Single:
                prop.SetValue(obj, Convert.ToSingle(v));
                break;
        }
    }

    private static void SetStrings(SaveObject w, Object obj)
    {
        var stringProps = GetStringProps(obj);
        var stringValues = w.S;

        foreach (var stringProp in stringProps)
        {
            stringProp.SetValue(obj, stringValues[0]);
            stringValues.RemoveAt(0);
        }
    }

    private static void SetObjects(SaveObject w, Object obj)
    {
        var objProps = GetObjectProps(obj);
        var objValues = w.O;

        foreach (var objProp in objProps)
        {
            var id = objValues[0];

            if (id > 0)
            {
                var o = Objects.All.Single(x => x.Id == id);
                objProp.SetValue(obj, o);
            }

            objValues.RemoveAt(0);
        }
    }
}