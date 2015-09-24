using System;
using System.Collections.Generic;
using System.Globalization;
using Nancy;
using Nancy.ErrorHandling;
using Nancy.ViewEngines;

namespace NancyDemo.Areas.Bootstrap
{
    public class StatusCodeHandler : IStatusCodeHandler
    {
        private readonly IViewRenderer _viewRenderer;
        private readonly IDictionary<HttpStatusCode, Action<HttpStatusCode, NancyContext>> _handlers = new Dictionary<HttpStatusCode, Action<HttpStatusCode, NancyContext>>();

        public StatusCodeHandler(IViewRenderer viewRenderer)
        {
            _viewRenderer = viewRenderer;
            _handlers[HttpStatusCode.NotFound] = RenderView;
            _handlers[HttpStatusCode.InternalServerError] = RenderView;
        }

        private void RenderView(HttpStatusCode statusCode, NancyContext context)
        {
            var viewName = ((int)statusCode).ToString(CultureInfo.InvariantCulture);
            var response = _viewRenderer.RenderView(context, viewName);
            context.Response.ContentType = response.ContentType;
            context.Response.Contents = response.Contents;
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return _handlers.ContainsKey(statusCode);
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            _handlers[statusCode](statusCode, context);
        }
    }
}