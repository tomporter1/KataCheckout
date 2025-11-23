using Kata.Checkout;
using NUnit.Framework;

namespace Kata.Tests;

// Item   Unit      Special
//        Price     Price
// --------------------------
// A     50       3 for 130
// B     30       2 for 45
// C     20
// D     15

public class CheckoutServiceTests
{
    ICheckoutService checkoutService;

    [SetUp]
    public void Setup()
    {
        checkoutService = new CheckoutService();
    }

    [Test]
    public void Empty_Basket_Returns_Zero()
    {
        Assert.That(checkoutService.Total(), Is.EqualTo(0));
    }

    [Test]
    public void Single_Item_Returns_Price()
    {
        checkoutService.ScanItem("A");
        Assert.That(checkoutService.Total(), Is.EqualTo(50));
    }

    [Test]
    public void Multiple_Items_Returns_Total()
    {
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("B");
        Assert.That(checkoutService.Total(), Is.EqualTo(80));
    }

    [Test]
    public void Multiple_Items_Returns_Total_Different_Order()
    {
        checkoutService.ScanItem("B");
        checkoutService.ScanItem("A");
        Assert.That(checkoutService.Total(), Is.EqualTo(80));
    }

    [Test]
    public void Invalid_Item_Returns_Zero()
    {
        checkoutService.ScanItem("X");
        Assert.That(checkoutService.Total(), Is.EqualTo(0));
    }

    [Test]
    public void Invalid_Item_Is_Ignored()
    {
        checkoutService.ScanItem("X");
        checkoutService.ScanItem("A");
        Assert.That(checkoutService.Total(), Is.EqualTo(50));
    }

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
    public void Remove_Item_Reduces_Total()
    {
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("A");
        Assert.That(checkoutService.Total(), Is.EqualTo(100));
        checkoutService.RemoveItem("A");
        Assert.That(checkoutService.Total(), Is.EqualTo(50));
    }

    [Test]
    public void Remove_Item_Reduces_Total_Multiple_Item_Types()
    {
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("B");
        checkoutService.ScanItem("A");
        Assert.That(checkoutService.Total(), Is.EqualTo(50 + 30 + 50));
        checkoutService.RemoveItem("A");
        Assert.That(checkoutService.Total(), Is.EqualTo(50 + 30));
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
}