using Kata.Checkout.Items;

namespace Kata.Checkout.Helpers;

public class DefaultProductCatalogue
{
    public static IProductCatalogue MakeDefault()
    {
        return new ProductCatalogue(new Dictionary<string, IProductPrice>
        {
            { "A", new MultiBuyProductPrice(50, 3, 130) },
            { "B", new MultiBuyProductPrice(30, 2, 45) },
            { "C", new BuyXGetYFree(20, 3, 1) },
            { "D", new ProductPrice(15) }
        });
    }
}