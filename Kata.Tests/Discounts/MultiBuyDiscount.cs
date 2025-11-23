using NUnit.Framework;

namespace Kata.Tests.Discounts;

public class MultiBuyDiscount : CheckoutTestFixture
{
    [Test]
    public void Special_Offer_Applies()
    {
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("A");
        Assert.That(checkoutService.Total(), Is.EqualTo(130));
    }

    [Test]
    public void Multiple_Special_Offers_Apply()
    {
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("B");
        checkoutService.ScanItem("B");
        Assert.That(checkoutService.Total(), Is.EqualTo(130 + 45));
    }

    [Test]
    public void Remove_Promo_Item_Removes_Promo()
    {
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("A");
        Assert.That(checkoutService.Total(), Is.EqualTo(130));
        checkoutService.RemoveItem("A");
        Assert.That(checkoutService.Total(), Is.EqualTo(50 + 50));
    }

    [Test]
    public void Multiple_Complete_Offers()
    {
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("A");
        Assert.That(checkoutService.Total(), Is.EqualTo(130 + 130));
    }

    [Test]
    public void Large_Quantity_Of_Single_Item()
    {
        for (int i = 0; i < 10; i++)
            checkoutService.ScanItem("A");
        Assert.That(checkoutService.Total(), Is.EqualTo(130 + 130 + 130 + 50));
    }

    [Test]
    public void Remove_From_Discounted_Bundle_Then_Readd()
    {
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("A");
        Assert.That(checkoutService.Total(), Is.EqualTo(130));

        checkoutService.RemoveItem("A");
        Assert.That(checkoutService.Total(), Is.EqualTo(100));

        checkoutService.ScanItem("A");
        Assert.That(checkoutService.Total(), Is.EqualTo(130));
    }
}