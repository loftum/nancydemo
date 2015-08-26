using Nancy;

namespace NancyDemo.AspNet
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = d => View["Index"];
        }
    }
}