using System;
using Nancy.Session;

namespace NancyDemo.Areas.Extensions
{
    public static class SessionExtensions
    {
        public static void TriggerUpdate(this ISession session)
        {
            session["Updated"] = DateTimeOffset.UtcNow;
        }
    }
}