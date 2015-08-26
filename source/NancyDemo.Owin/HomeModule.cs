using Nancy;
using NancyDemo.Owin.Models;

namespace NancyDemo.Owin
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = d => new Dude {Name = "The Dude", Things = new[] {"Bowling ball", "Anvil"}};
        }
    }
}