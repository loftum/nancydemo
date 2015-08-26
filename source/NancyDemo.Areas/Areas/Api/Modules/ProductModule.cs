using Nancy;
using NancyDemo.Areas.Authentication;
using NancyDemo.Lib.Data;
using NancyDemo.Lib.Domain;

namespace NancyDemo.Areas.Areas.Api.Modules
{
    public class ProductModule : ApiModuleBase
    {
        private readonly IRepository _repo;

        public ProductModule(IRepository repo) : base("products")
        {
            _repo = repo;
            Get["/"] = GetProducts;
            Get["{id}"] = GetProduct;
            Get["secret"] = GetSecret;
        }

        private object GetSecret(dynamic arg)
        {
            this.RequiresBasicAuth("larry", "ken sent me");
            return Data(new Secret("Pølse"));
        }

        private object GetProduct(dynamic arg)
        {
            int id = arg.id;
            var product = _repo.GetProduct(id);
            if (product == null)
            {
                return HttpStatusCode.NotFound;
            }
            return Data(product);
        }

        private object GetProducts(dynamic arg)
        {
            return Data(_repo.GetAllProducts());
        }
    }
}