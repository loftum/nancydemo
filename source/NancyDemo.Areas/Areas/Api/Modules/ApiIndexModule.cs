using System.Linq;
using Nancy.Routing;

namespace NancyDemo.Areas.Areas.Api.Modules
{
    public class ApiIndexModule : ApiModuleBase
    {
        private readonly IRouteCacheProvider _routeCacheProvider;

        public ApiIndexModule(IRouteCacheProvider routeCacheProvider)
        {
            _routeCacheProvider = routeCacheProvider;
            Get["/"] = GetRoutes;
        }

        private object GetRoutes(dynamic arg)
        {
            var cache = _routeCacheProvider.GetCache().Values.SelectMany(v => v).Select(v => v.Item2)
                .Select(v => v.Path)
                .ToList();
            return Data(cache);
        }
    }
}