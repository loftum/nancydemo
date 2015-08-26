using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using Microsoft.Owin;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Owin;
using Nancy.Security;
using NancyDemo.Areas.Areas.Admin.Models;
using NancyDemo.Areas.Authentication;

namespace NancyDemo.Areas.Areas.Admin.Modules
{
    public class UserModule : NancyModule
    {
        protected IOwinContext OwinContext
        {
            get
            {
                var environment = Context.GetOwinEnvironment() ?? new Dictionary<string, object>();
                return new OwinContext(environment);
            }
        }

        public UserModule() : base("user")
        {
            Get["login"] = GetLogin;
            Post["login"] = ValidateCsrfAnd(DoLogIn);
            Get["logout"] = LogOut;
        }

        private object LogOut(object arg)
        {
            OwinContext.Authentication.SignOut(AuthenticationType.Admin);
            return HttpStatusCode.Unauthorized;
        }

        private Func<dynamic, object> ValidateCsrfAnd(Func<dynamic, object> func)
        {
            return d =>
            {
                this.ValidateCsrfToken();
                return func(d);
            };
        }

        private object DoLogIn(object arg)
        {
            var input = this.Bind<LoginModel>();
            if (input.IsValid())
            {
                var identity = new ClaimsIdentity(GetClaimsFor(input), AuthenticationType.Admin);
                OwinContext.Authentication.SignIn(identity);
                return Response.AsRedirect("~/");
            }
            throw new AbandonedMutexException("Please go away.");
        }



        private static IEnumerable<Claim> GetClaimsFor(LoginModel input)
        {
            yield return new Claim(ClaimTypes.Name, input.Username);
            yield return new Claim(ClaimTypes.NameIdentifier, "42");
        }

        private object GetLogin(dynamic arg)
        {
            return View["Login", new LoginModel()];
        }
    }
}