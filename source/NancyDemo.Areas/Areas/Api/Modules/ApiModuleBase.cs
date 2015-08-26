using Nancy;
using Nancy.Responses.Negotiation;

namespace NancyDemo.Areas.Areas.Api.Modules
{
    public abstract class ApiModuleBase : NancyModule
    {
        protected ApiModuleBase()
        {
        }

        protected ApiModuleBase(string modulePath) : base(modulePath)
        {
        }

        protected Negotiator Data(object model)
        {
            return Negotiate.WithModel(model)
                .WithAllowedMediaRange(new MediaRange("application/xml"))
                .WithAllowedMediaRange(new MediaRange("application/json"));
        }
    }
}