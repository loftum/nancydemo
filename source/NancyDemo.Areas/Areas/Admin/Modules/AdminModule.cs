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
            Get["logout"] = LogOut;
        }

        private object LogOut(object arg)
        {
            throw new System.NotImplementedException();
        }
    }
}