using System;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.Extensions;

namespace NancyDemo.Areas.Authentication
{
    public static class AuthenticationExtensions
    {
        public static void RequiresBasicAuth(this INancyModule module, string username, string password)
        {
            module.AddBeforeHookOrExecute(c =>
            {
                var header = c.Request.Headers["Authorization"].FirstOrDefault();
                if (header == null)
                {
                    return HttpStatusCode.Unauthorized;
                }
                var auth = "Basic " + string.Join(":", username, password);
                var base64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(auth));
                if (!string.Equals(base64, header, StringComparison.InvariantCulture))
                {
                    return HttpStatusCode.Unauthorized;
                }
                return null;
            }, "Requires Authentication");
        }
    }
}