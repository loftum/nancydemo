using System;

namespace NancyDemo.Lib.Extensions
{
    public static class ExceptionExtensions
    {
        public static Exception GetMostInner(this Exception exception)
        {
            var inner = exception;
            while (inner.InnerException != null)
            {
                inner = inner.InnerException;
            }
            return inner;
        }
    }
}