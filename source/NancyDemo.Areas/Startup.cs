using System.Threading.Tasks;
using Microsoft.Owin.Security.Cookies;
using Nancy.Owin;
using NancyDemo.Areas.Authentication;
using NancyDemo.Areas.Bootstrap;
using NancyDemo.Lib.Extensions;
using Owin;

namespace NancyDemo.Areas
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/Data", a => { })
                .Map("/admin", a => a
                .UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    CookieName = "AdminUser", 
                    AuthenticationType = AuthenticationType.Admin
                })
                .Use<GurgleAuthentication>()
                .UseNancy(new NancyOptions
                {
                    Bootstrapper = new AreaBootstrapper("Admin")
                }))
                .Map("/api", a => a.UseNancy(new NancyOptions
                {
                    Bootstrapper = new AreaBootstrapper("Api")
                }))
                .UseNancy(new NancyOptions
                {
                    //PerformPassThrough = c => c.Response.StatusCode == HttpStatusCode.ImATeapot,
                    Bootstrapper = new AreaBootstrapper("Public")
                })
                .Use((c, next) =>
                {
                    c.Response.Body = "I am a teapot".ToStream();
                    c.Response.ReasonPhrase = "I am a teapot";
                    c.Response.StatusCode = 418;
                    return Task.FromResult(0);
                });
        }
    }
}