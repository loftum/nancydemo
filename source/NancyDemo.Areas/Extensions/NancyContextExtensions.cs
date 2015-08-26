using Nancy;
using NancyDemo.Areas.Areas.Public.Models;

namespace NancyDemo.Areas.Extensions
{
    public static class NancyContextExtensions
    {
        public static Basket GetBasket(this NancyContext context)
        {
            if (context == null)
            {
                return new Basket();
            }
            var basket = context.Request.Session["Basket"] as Basket;
            if (basket == null)
            {
                basket = new Basket();
                context.Request.Session["Basket"] = basket;
            }
            return basket;
        }
    }
}