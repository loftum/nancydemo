using System.Collections.Generic;
using Nancy.ViewEngines.Razor;

namespace NancyDemo.Vanilla.Bootstrap
{
    // Can also be configured in web.config

    public class RazorConfig : IRazorConfiguration
    {
        public IEnumerable<string> GetAssemblyNames()
        {
            yield return "Nancy";
            yield return "Nancy.ViewEngines.Razor";
            yield return "NancyDemo.Vanilla";
            yield return "NancyDemo.Lib";
        }

        public IEnumerable<string> GetDefaultNamespaces()
        {
            yield return "Nancy";
            yield return "Nancy.ViewEngines.Razor";
            yield return "NancyDemo.Vanilla";
            yield return "NancyDemo.Lib";
        }

        public bool AutoIncludeModelNamespace { get { return true; } }
    }
}