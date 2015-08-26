using System.Threading.Tasks;
using Microsoft.Owin;

namespace NancyDemo.Areas.Authentication
{
    public class SierraAuthentication : OwinMiddleware
    {
        public SierraAuthentication(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            await Next.Invoke(context);
            if (context.Response.StatusCode == 401)
            {
                context.Response.Redirect("/admin/user/login");
            }
        }
    }
}