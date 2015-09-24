using NancyDemo.Lib.Extensions;

namespace NancyDemo.Lib
{
    public class Generate
    {
        private static readonly string[] EmptyBasketMessages =
        {
            "Nah, don't want it anyway",
            "Empty the basket",
            "Clear",
            "Get rid of everything",
            "Oooh, too expensive",
            "On second thought..."
        };

        public static string EmptyBasketMessage()
        {
            return EmptyBasketMessages.Random();
        }

        private static readonly string[] BuyMessages =
        {
            "Buy one!",
            "Get one!",
            "Take one!",
            "Me wants!"
        };

        public static string BuyMessage()
        {
            return BuyMessages.Random();
        }
    }
}