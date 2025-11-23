namespace Kata.Checkout.Items;

public class MultiBuyItem(int unitPrice, int discountQuantity, int discountPrice) : Item(unitPrice)
{
    public override int CalculatePrice(int quantity)
    {
        if (quantity < discountQuantity)
            return base.CalculatePrice(quantity);

        int discountedPrice = 0;
        int remainingQuantity = quantity;
        while (remainingQuantity > 0)
        {
            if (remainingQuantity >= discountQuantity)
            {
                discountedPrice += discountPrice;
                remainingQuantity -= discountQuantity;
            }
            else
            {
                discountedPrice += base.CalculatePrice(remainingQuantity);
                remainingQuantity = 0;
            }
        }
        return discountedPrice;
    }
}