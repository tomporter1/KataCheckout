namespace Kata.Checkout.Items;

public class MultiBuyItem(int unitPrice, int discountQuantity, int discountPrice) : Item(unitPrice)
{
    public override int CalculatePrice(int quantity)
    {
        if (quantity < discountQuantity)
            return base.CalculatePrice(quantity);
        return discountPrice;
    }
}