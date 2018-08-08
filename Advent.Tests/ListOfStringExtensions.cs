using System.Collections.Generic;
using NUnit.Framework;

namespace Advent.Tests
{
    public static class ListOfStringExtensions
    {
        public static void ShouldContain(this IList<string> list, string value)
        {
            Assert.IsTrue(list.Contains(value));
        }

        public static void ShouldNotContain(this IList<string> list, string value)
        {
            Assert.IsFalse(list.Contains(value));
        }

        public static void CountShouldBe(this IList<string> list, int value)
        {
            Assert.AreEqual(value, list.Count);
        }
    }
}
