namespace Kata.Checkout.Items;

public class Item(float unitPrice) : IItem
{
    public string Name { get; }

    public virtual float CalculatePrice(int quantity)
    {
        return unitPrice * quantity;
    }
}