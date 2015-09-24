using Nancy;
using Nancy.Security;

namespace NancyDemo.Areas.Areas.Admin.Modules
{
    public class AdminModule : NancyModule
    {
        public AdminModule()
        {
            this.RequiresAuthentication();
            Get["/"] = d => View["Index"];
        }
    }
}