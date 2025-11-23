namespace Kata.Checkout.Items;

public class ProductPrice(int unitPrice) : IProductPrice
{
    public virtual int CalculatePrice(int quantity)
    {
        return unitPrice * quantity;
    }
}