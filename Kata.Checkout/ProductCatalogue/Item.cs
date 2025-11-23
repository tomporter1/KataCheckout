namespace Kata.Checkout.Items;

public class Item(int unitPrice) : IItem
{
    public string Name { get; }

    public virtual int CalculatePrice(int quantity)
    {
        return unitPrice * quantity;
    }
}