using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Nancy;
using Nancy.ErrorHandling;
using Nancy.Extensions;
using Nancy.IO;
using Nancy.Responses.Negotiation;
using Nancy.ViewEngines;
using NancyDemo.Lib.Extensions;

namespace NancyDemo.Areas.Bootstrap
{
    public class StatusCodeHandler : IStatusCodeHandler
    {
        private readonly List<IResponseProcessor> _processors; 
        private readonly IViewRenderer _viewRenderer;
        private readonly IDictionary<HttpStatusCode, Action<HttpStatusCode, NancyContext>> _handlers = new Dictionary<HttpStatusCode, Action<HttpStatusCode, NancyContext>>();

        public StatusCodeHandler(IViewRenderer viewRenderer, IEnumerable<IResponseProcessor> processors)
        {
            _viewRenderer = viewRenderer;
            _handlers[HttpStatusCode.NotFound] = RenderView;
            _handlers[HttpStatusCode.InternalServerError] = RenderView;
            _processors = processors
                .Where(p => p is JsonProcessor || p is XmlProcessor)
                .ToList();
        }

        private void RenderView(HttpStatusCode statusCode, NancyContext context)
        {
            var response = GetResponse(statusCode, context);
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

        private Response GetResponse(HttpStatusCode statusCode, NancyContext context)
        {
            var responseInfo = new ResponseInfo
            {
                StatusCode = (int)statusCode,
                Message = statusCode.ToString(),
                Reason = StaticConfiguration.DisableErrorTraces ? null : context.GetExceptionDetails()
            };
            var nonHtmlResponse = TryProcessNonHtml(responseInfo, context);
            if (nonHtmlResponse != null)
            {
                return nonHtmlResponse;
            }
            var viewName = ((int)statusCode).ToString(CultureInfo.InvariantCulture);
            var response = _viewRenderer.RenderView(context, viewName);
            return response;
        }

        private Response TryProcessNonHtml(ResponseInfo responseInfo, NancyContext context)
        {
            var firstHeader = context.Request.Headers.Accept.Select(a => new MediaRange(a.Item1)).FirstOrDefault();
            if (firstHeader == null)
            {
                return null;
            }
            var processor = GetProcessor(firstHeader, responseInfo, context);
            return processor == null
                ? null
                : processor.Process(firstHeader, (dynamic)responseInfo, context);
        }

        private IResponseProcessor GetProcessor(MediaRange acceptHeader, ResponseInfo model, NancyContext context)
        {
            return _processors.FirstOrDefault(p => IsMatch(p.CanProcess(acceptHeader, (dynamic)model, context)));
        }

        private static bool IsMatch(ProcessorMatch match)
        {
            return match.RequestedContentTypeResult.In(MatchResult.ExactMatch) &&
                   match.ModelResult.In(MatchResult.ExactMatch, MatchResult.DontCare);
        }


        public class ResponseInfo
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public string Reason { get; set; }
        }
    }
}