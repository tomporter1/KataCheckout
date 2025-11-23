namespace Kata.Checkout.Items;

public class BuyXGetYFree(int basePrice, int buyQuantity, int freeQuantity) : Item(basePrice)
{
    public override int CalculatePrice(int quantity)
    {
        return base.CalculatePrice(quantity);
    }
}