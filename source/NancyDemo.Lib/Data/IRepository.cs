using System.Collections.Generic;
using NancyDemo.Lib.Domain;

namespace NancyDemo.Lib.Data
{
    public interface IRepository
    {
        IList<Product> GetAllProducts();
        Product GetProduct(int id);
    }
}