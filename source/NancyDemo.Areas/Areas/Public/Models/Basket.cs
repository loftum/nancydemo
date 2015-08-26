using System;
using System.Collections.Generic;
using System.Linq;
using NancyDemo.Lib.Domain;

namespace NancyDemo.Areas.Areas.Public.Models
{
    [Serializable]
    public class Basket
    {
        public List<Product> Products { get; set; }

        public Basket()
        {
            Products = new List<Product>();
        }

        public bool IsEmpty()
        {
            return !Products.Any();
        }

        public decimal GetSum()
        {
            return Products.Sum(p => p.Price);
        }
    }
}