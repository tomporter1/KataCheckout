namespace Kata.Checkout.Items;

/// <summary>
/// If the customer buys X of this productPrice, they get Y free. For simplicity if they only scan X items and no more, then this won't include the free items.
/// </summary>
public class BuyXGetYFree(int basePrice, int buyQuantity, int freeQuantity) : ProductPrice(basePrice)
{
    public override int CalculatePrice(int quantity)
    {
        if (quantity < buyQuantity)
            return base.CalculatePrice(quantity);
        int offerSets = (int)Math.Floor(quantity / (float)(buyQuantity + freeQuantity));
        
        int nonSetItems = quantity - (offerSets * (buyQuantity + freeQuantity));

        return (nonSetItems * basePrice) + (offerSets * (buyQuantity * basePrice));
    }
}