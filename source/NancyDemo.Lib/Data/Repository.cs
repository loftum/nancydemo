using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using NancyDemo.Lib.Domain;
using NancyDemo.Lib.Extensions;
using Newtonsoft.Json;

namespace NancyDemo.Lib.Data
{
    public class Repository : IRepository
    {
        private static readonly List<Product> Products;

        static Repository()
        {
            var client = new WebClient();
            var dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Products");
            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }
            var productFile = Path.Combine(dataDir, "Products.json");

            if (!File.Exists(productFile))
            {
                
                var random = new Random();
                Products = Enumerable.Range(1, 30)
                    .Select(i => new Product
                    {
                        Id = i,
                        Name = string.Join(" ", client.DownloadString("http://www.classnamer.com/index.txt?generator=generic").SplitCamelCase()),
                        Price = random.Next(599, 129999)
                    })
                    .ToList();
                File.WriteAllText(productFile, JsonConvert.SerializeObject(Products, Formatting.Indented));
            }
            else
            {
                Products = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(productFile));
            }

            var imageDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content", "Images");
            
            if (!Directory.Exists(imageDir))
            {
                Directory.CreateDirectory(imageDir);
            }
            var count = Directory.GetFiles(imageDir, "*.jpg").Length;
            if (count < 30)
            {
                for (var ii = count; ii < 30; ii++)
                {
                    var filePath = Path.Combine(imageDir, string.Format("{0}.jpeg", ii+1));
                    var bytes = client.DownloadData("http://lorempixel.com/200/200/technics");
                    File.WriteAllBytes(filePath, bytes);
                }
            }
        }

        public IList<Product> GetAllProducts()
        {
            return Products;
        }


        public Product GetProduct(int id)
        {
            return GetAllProducts().FirstOrDefault(p => p.Id == id);
        }
    }
}