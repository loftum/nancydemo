using Owin;

namespace NancyDemo.Vanilla
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}