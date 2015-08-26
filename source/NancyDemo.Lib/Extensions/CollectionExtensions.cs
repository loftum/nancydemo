using System;
using System.Collections.Generic;
using System.Linq;

namespace NancyDemo.Lib.Extensions
{
    public static class CollectionExtensions
    {
        private static readonly Random Randoms = new Random();

        public static T Random<T>(this IList<T> collection)
        {
            if (collection == null || !collection.Any())
            {
                return default(T);
            }
            return collection[Randoms.Next(0, collection.Count - 1)];
        }

        public static bool In<T>(this T item, params T[] items)
        {
            return items.Contains(item);
        }
    }
}