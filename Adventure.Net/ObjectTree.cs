using System;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net;
public class ObjectTree
{
    private readonly Object _root;
    private readonly bool _includeAbsent;
    private bool _lit;

    private Func<Object, bool> _baseFilter;

    public ObjectTree(Object root, bool includeAbsent = true)
    {
        _root = root;
        _includeAbsent = includeAbsent;

        _baseFilter = (current) =>
        {
            return
                current != null &&
                (_includeAbsent || !current.Absent) &&
                (current.Parent is not Container container || container.ContentsVisible);
        };

    }

    public bool Any(Func<Object, bool> filter = null)
    {
        return BreakOnTrue(_root, filter);
    }

    public List<Object> GetObjects(Func<Object, bool> filter = null)
    {
        var result = new List<Object>();
        Traverse(_root, result);

        filter ??= ((_) => true);

        return [.. result.Where(filter)];
    }

    public List<Object> GetObjects(out bool lit)
    {
        var result = new List<Object>();
        Traverse(_root, result);

        lit = _lit;

        return result;
    }

    private void Traverse(Object current, List<Object> result)
    {
        if (!_baseFilter(current))
        {
            return;
        }

        if (current.Light)
        {
            _lit = true;
        }

        result.Add(current);

        if (current.Children != null)
        {
            foreach (var child in current.Children)
            {
                Traverse(child, result);
            }
        }
    }

    private bool BreakOnTrue(Object current, Func<Object, bool> filter = null)
    {
        if (!_baseFilter(current))
        {
            return false;
        }

        if (filter(current))
        {
            return true;
        }

        if (current.Children != null)
        {
            foreach (var child in current.Children)
            {
                var result = BreakOnTrue(child, filter);
                
                if (result)
                {
                    return true;
                }
            }
        }

        return false;
    }
}