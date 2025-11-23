using Kata.Checkout;
using Kata.Checkout.Helpers;
using Kata.Checkout.Items;
using NUnit.Framework;

namespace Kata.Tests.Discounts;

// ProductPrice   Unit      Special
//        ProductPrice     ProductPrice
// --------------------------
// A     50       3 for 130
// B     30       2 for 45
// C     20       Buy 3 get 1 free
// D     15

public class BuyXGetYFreeTests : CheckoutTestFixture
{
    [Test]
    public void BuyXGetYFree_Applies()
    {
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");
        Assert.That(checkoutService.Total(), Is.EqualTo(20 * 3));
    }

    [Test]
    public void BuyXGetYFree_Multiple_Offers_Applies()
    {
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");

        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");

        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");

        Assert.That(checkoutService.Total(), Is.EqualTo(20 * 9));
    }

    [Test]
    public void BuyXGetYFree_Multiple_Offers_Plus_Extra_Applies()
    {
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");

        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");

        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");

        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");

        Assert.That(checkoutService.Total(), Is.EqualTo((20 * 9) + 40));
    }

    [Test]
    public void BuyXGetYFree_Not_Enough_Items_To_Apply()
    {
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");
        Assert.That(checkoutService.Total(), Is.EqualTo(20 * 2));
    }

    [Test]
    public void BuyXGetYFree_Not_Applies()
    {
        checkoutService.ScanItem("D");
        checkoutService.ScanItem("D");
        checkoutService.ScanItem("D");
        checkoutService.ScanItem("D");
        Assert.That(checkoutService.Total(), Is.EqualTo(15 * 4));
    }

    [Test]
    public void BuyXGetYFree_Applies_With_Other_Items()
    {
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("B");
        Assert.That(checkoutService.Total(), Is.EqualTo((20 * 3) + 50 + 30));
    }
}