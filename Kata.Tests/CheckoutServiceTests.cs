using Kata.Checkout;
using Kata.Checkout.Helpers;
using Kata.Checkout.Items;
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
    IProductCatalogue productCatalogue;

    [SetUp]
    public void Setup()
    {
        productCatalogue = DefaultProductCatalogue.MakeDefault();
        checkoutService = new CheckoutService(productCatalogue);
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

    [Test]
    public void Remove_Non_Existent_Item_Does_Nothing()
    {
        checkoutService.RemoveItem("A");
        Assert.That(checkoutService.Total(), Is.EqualTo(0));
    }

    [Test]
    public void Mixed_Items_With_Some_On_Offer()
    {
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("B");
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("D");
        Assert.That(checkoutService.Total(), Is.EqualTo(50 + 30 + 20 + 15));
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
    public void Offer_With_Extra_Items()
    {
        // 3 for 130 + 1 at 50
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("A");
        Assert.That(checkoutService.Total(), Is.EqualTo(130 + 50));
    }

    [Test]
    public void B_Offer_With_Extra_Items()
    {
        checkoutService.ScanItem("B");
        checkoutService.ScanItem("B");
        checkoutService.ScanItem("B");
        Assert.That(checkoutService.Total(), Is.EqualTo(45 + 30));
    }

    [Test]
    public void Remove_Item_From_Non_Existent_Type_Does_Nothing()
    {
        checkoutService.ScanItem("A");
        checkoutService.RemoveItem("B");
        Assert.That(checkoutService.Total(), Is.EqualTo(50));
    }

    [Test]
    public void Remove_Multiple_Items_Of_Same_Type()
    {
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("A");
        checkoutService.RemoveItem("A");
        checkoutService.RemoveItem("A");
        Assert.That(checkoutService.Total(), Is.EqualTo(50 + 50));
    }

    [Test]
    public void Remove_All_Items_Returns_To_Zero()
    {
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("B");
        checkoutService.RemoveItem("A");
        checkoutService.RemoveItem("B");
        Assert.That(checkoutService.Total(), Is.EqualTo(0));
    }

    [Test]
    public void Complex_Basket_With_Mixed_Operations()
    {
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("A");
        checkoutService.ScanItem("B");
        checkoutService.ScanItem("B");
        checkoutService.ScanItem("C");
        Assert.That(checkoutService.Total(), Is.EqualTo(130 + 45 + 20));

        checkoutService.RemoveItem("A");
        Assert.That(checkoutService.Total(), Is.EqualTo(50 + 50 + 45 + 20));

        checkoutService.ScanItem("D");
        checkoutService.ScanItem("D");
        Assert.That(checkoutService.Total(), Is.EqualTo(50 + 50 + 45 + 20 + 30));
    }

    [Test]
    public void Items_Without_Discount()
    {
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");
        checkoutService.ScanItem("C");
        Assert.That(checkoutService.Total(), Is.EqualTo(60));
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