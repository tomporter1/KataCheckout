using Kata.Checkout;
using Kata.Checkout.Helpers;
using Kata.Checkout.Items;
using NUnit.Framework;

namespace Kata.Tests.Discounts;

public class CheckoutTestFixture
{
   protected ICheckoutService checkoutService;
   private IProductCatalogue productCatalogue;

    [SetUp]
    public void Setup()
    {
        productCatalogue = DefaultProductCatalogue.MakeDefault();
        checkoutService = new CheckoutService(productCatalogue);
    }
}