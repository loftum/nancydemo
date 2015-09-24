using Nancy;

namespace NancyDemo
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = d => Response.AsText("<html><body>Self hosted on... err... self!</body></html>", "text/html");
        }
    }
}