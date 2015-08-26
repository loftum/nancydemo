using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Autofac;
using Autofac.Core.Lifetime;
using Microsoft.Owin;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Owin;
using Nancy.Security;
using Nancy.Session;
using NancyDemo.Areas.Authentication;

namespace NancyDemo.Areas.Bootstrap
{
    public class AreaBootstrapper : AutofacNancyBootstrapper
    {
        private readonly string _rootNamespace;
        private readonly string _area;

        private readonly IRootPathProvider _rootPathProvider;
        protected override IRootPathProvider RootPathProvider
        {
            get { return _rootPathProvider; }
        }

        private static readonly IDictionary<string, IList<ModuleRegistration>> AreaModules = new Dictionary<string, IList<ModuleRegistration>>();

        private static readonly byte[] FavIconBytes;
        protected override byte[] FavIcon
        {
            get { return FavIconBytes; }
        }

        static AreaBootstrapper()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "favicon.ico");
            FavIconBytes = File.Exists(path) ? File.ReadAllBytes(path) : new byte[0];
        }

        protected override IEnumerable<ModuleRegistration> Modules
        {
            get
            {
                if (!AreaModules.ContainsKey(_area))
                {
                    AreaModules[_area] = GetType().Assembly.GetTypes()
                        .Where(
                            t =>
                                typeof (INancyModule).IsAssignableFrom(t) &&
                                (t.Namespace ?? "").StartsWith(_rootNamespace) && t.IsClass && !t.IsAbstract)
                        .Select(t => new ModuleRegistration(t))
                        .ToList();
                }
                return AreaModules[_area];
            }
        }

        public AreaBootstrapper(string area)
        {
            _area = area;
            _rootNamespace = string.Join(".", "NancyDemo.Areas.Areas", area);
            _rootPathProvider = new AreaRootPathProvider(area);
        }

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            Csrf.Enable(pipelines);
            FruitOfTheDay.Enable(pipelines);
            CookieBasedSessions.Enable(pipelines);
        }

        protected override ILifetimeScope GetApplicationContainer()
        {
            return IoC.Container.BeginLifetimeScope(_rootNamespace, b => b.RegisterTypes(Modules.Select(m => m.ModuleType).ToArray()));
        }

        protected override INancyModule GetModule(ILifetimeScope container, Type moduleType)
        {
            var module = container.Resolve(moduleType);
            return (INancyModule) module;
        }

        protected override ILifetimeScope CreateRequestContainer(NancyContext context)
        {
            return ApplicationContainer.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
        }

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
        {
            SignInOwinUser(context);
        }

        private static void SignInOwinUser(NancyContext context)
        {
            var principal = new OwinContext(context.GetOwinEnvironment() ?? new Dictionary<string, object>()).Authentication.User;
            if (principal != null && principal.Identity.IsAuthenticated)
            {
                context.CurrentUser = new DemoUser(principal.FindFirst(c => c.Type == ClaimTypes.Name).Value, principal.Claims.Select(c => string.Join(":", c.Type, c.Value)));
            }
        }
    }
}