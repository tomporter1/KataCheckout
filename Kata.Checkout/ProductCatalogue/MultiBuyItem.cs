namespace Kata.Checkout.Items;

public class MultiBuyItem(float unitPrice, int discountQuantity, float discountPrice) : Item(unitPrice)
{
    public override float CalculatePrice(int quantity)
    {
        if (quantity > discountQuantity)
            return base.CalculatePrice(quantity);
        return discountPrice;
    }
}