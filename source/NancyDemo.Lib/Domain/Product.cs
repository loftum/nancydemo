using System;

namespace NancyDemo.Lib.Domain
{
    [Serializable]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public string GetImageFile()
        {
            return string.Format("{0}.jpeg", Id);
        }
    }
}