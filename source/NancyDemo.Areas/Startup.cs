using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Cookies;
using Nancy;
using Nancy.Owin;
using NancyDemo.Areas.Authentication;
using NancyDemo.Areas.Bootstrap;
using Owin;

namespace NancyDemo.Areas
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app
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
                    })
                )
                .Map("/api", a => a
                    .UseNancy(new NancyOptions
                    {
                        Bootstrapper = new AreaBootstrapper("Api")
                    })
                )
                .UseNancy(new NancyOptions
                {
                    PerformPassThrough = c => c.Response.StatusCode == HttpStatusCode.ImATeapot,
                    Bootstrapper = new AreaBootstrapper("Public")
                })
                .Use((c, next) =>
                {
                    using (var writer = new StreamWriter(c.Response.Body))
                    {
                        writer.Write("You can't get coffee. I am a teapot!");
                    }
                    c.Response.ContentType = "text/plain";
                    c.Response.ReasonPhrase = "I am a teapot";
                    c.Response.StatusCode = 418;
                    return Task.FromResult(0);
                });
        }
    }
}