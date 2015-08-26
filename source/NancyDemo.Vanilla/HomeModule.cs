using Nancy;
using NancyDemo.Lib.Data;

namespace NancyDemo.Vanilla
{
    public class HomeModule : NancyModule
    {
        private readonly IRepository _repo;

        public HomeModule(IRepository repo)
        {
            _repo = repo;
            Get["/"] = d => View["Index", _repo.GetAllProducts()];
            Get["product/{id}"] = GetProduct;
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
    }
}