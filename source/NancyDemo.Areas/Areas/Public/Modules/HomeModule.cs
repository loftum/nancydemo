using System;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nancy;
using NancyDemo.Areas.Areas.Public.Models;
using NancyDemo.Areas.Extensions;
using NancyDemo.Lib.Data;
using NancyDemo.Lib.Domain;

namespace NancyDemo.Areas.Areas.Public.Modules
{
    public class HomeModule : NancyModule
    {
        private readonly IRepository _repo;

        public HomeModule(IRepository repo)
        {
            _repo = repo;
            Get["/"] = Index;
            Get["product/{id}"] = GetProduct;
//            Get["product/1"] = d => "One";
//            Get["product/1"] = d => "Ein";
            Get["coffee"] = d => HttpStatusCode.ImATeapot;
            Get["Error"] = ThrowError;
            Post["buy/{id}"] = BuyProduct;
            Post["emptyBasket"] = EmptyBasket;
            Get["async", runAsync: true] = GetSomethingAsync;

            if (ConfigurationManager.AppSettings["Hacks.Enabled"] == "Yeah!")
            {
                Get["hack"] = d => Response.AsImage("Content/hack.png");
            }
        }

        private async Task<object> GetSomethingAsync(dynamic arg, CancellationToken token)
        {
            await Task.Delay(10, token);
            return Response.AsText("<div>This was done async</div>", "text/html");
        }

        private object EmptyBasket(object arg)
        {
            Basket.Products.Clear();
            Session.TriggerUpdate();
            return Response.AsRedirect(Referer);
        }

        protected Basket Basket { get { return Context.GetBasket(); } }

        private object BuyProduct(dynamic arg)
        {
            int id = arg.id;
            var product = _repo.GetProduct(id);
            Basket.Products.Add(new Product{Id = product.Id, Name = product.Name, Price = product.Price});
            Session.TriggerUpdate();
            return Response.AsRedirect(Referer);
        }

        private object GetProduct(dynamic arg)
        {
            int id = arg.id;
            var product = _repo.GetProduct(id);
            if (product == null)
            {
                return HttpStatusCode.NotFound;
            }
            return product;
        }

        private static object ThrowError(object arg)
        {
            throw new Exception("This is error");
        }

        private object Index(dynamic arg)
        {
            var products = _repo.GetAllProducts();
            return View["Index", products];
        }

        private string Referer
        {
            get { return Context.Request.Headers["Referer"].FirstOrDefault() ?? "/"; }
        }
    }
}