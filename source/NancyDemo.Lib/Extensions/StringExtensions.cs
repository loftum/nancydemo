using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NancyDemo.Lib.Extensions
{
    public static class StringExtensions
    {
        public static Stream ToStream(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            sw.Write(value);
            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        public static IEnumerable<string> SplitCamelCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                yield break;
            }
            var builder = new StringBuilder();
            foreach(var c in value)
            {
                if (char.IsUpper(c))
                {
                    if (builder.Length > 0)
                    {
                        yield return builder.ToString();
                        builder.Clear();
                    }
                }
                builder.Append(c);
            }
            if (builder.Length > 0)
            {
                yield return builder.ToString();
            }
        }
    }
}