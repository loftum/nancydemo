using Nancy;
using Nancy.Bootstrapper;
using Nancy.Cookies;
using NancyDemo.Lib.Extensions;

namespace NancyDemo.Areas.Bootstrap
{
    public static class FruitOfTheDay
    {
        private static readonly string[] Fruits = {"Banana", "Grape", "Orange", "Apple"};

        public static void Enable(IPipelines pipelines)
        {
            pipelines.AfterRequest.AddItemToStartOfPipeline(AddFruit);
        }

        private static void AddFruit(NancyContext context)
        {
            if (context == null || context.Request == null || context.Response == null)
            {
                return;
            }
            if (!context.Request.Cookies.ContainsKey("FruitOfTheDay"))
            {
                context.Response.Cookies.Add(new NancyCookie("FruitOfTheDay", Fruits.Random()));
            }
        }
    }
}