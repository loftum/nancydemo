using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Nancy;
using Nancy.ErrorHandling;
using Nancy.ViewEngines;

namespace NancyDemo.Areas.Bootstrap
{
    public class StatusCodeHandler : IStatusCodeHandler
    {
        private readonly IViewRenderer _viewRenderer;
        private readonly IDictionary<HttpStatusCode, Action<HttpStatusCode, NancyContext>> _pages = new Dictionary<HttpStatusCode, Action<HttpStatusCode, NancyContext>>();

        public StatusCodeHandler(IViewRenderer viewRenderer)
        {
            _viewRenderer = viewRenderer;
            _pages[HttpStatusCode.ImATeapot] = Teapot;
            _pages[HttpStatusCode.InternalServerError] = RenderView;
        }

        private void RenderView(HttpStatusCode statusCode, NancyContext context)
        {
            var viewName = ((int)statusCode).ToString(CultureInfo.InvariantCulture);
            var response = _viewRenderer.RenderView(context, viewName);
            context.Response.ContentType = response.ContentType;
            context.Response.Contents = response.Contents;
        }

        private void Teapot(HttpStatusCode statusCode, NancyContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Contents = s =>
            {
                var writer = new StreamWriter(s);
                writer.Write(_pages[statusCode]);
                writer.Flush();
            };
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return _pages.ContainsKey(statusCode);
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            _pages[statusCode](statusCode, context);
        }
    }
}