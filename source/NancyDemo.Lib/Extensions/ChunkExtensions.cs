using System;
using System.Collections.Generic;

namespace NancyDemo.Lib.Extensions
{
    public static class ChunkExtensions
    {
        public static IEnumerable<IEnumerable<T>> InChunksOf<T>(this IEnumerable<T> items, int chunkSize)
        {
            if (chunkSize < 1)
            {
                throw new ArgumentException("Gawd...", "chunkSize");
            }
            var enumerator = items.GetEnumerator();
            while ( enumerator.MoveNext())
            {
                yield return enumerator.GetNext(chunkSize);
            }
        }

        public static IEnumerable<T> GetNext<T>(this IEnumerator<T> enumerator, int count)
        {
            if (count < 1)
            {
                throw new ArgumentException("Gawd...", "count");
            }
            var returned = 0;
            do
            {
                yield return enumerator.Current;
                returned++;
            } while (returned <= count && enumerator.MoveNext());

        }
    }
}