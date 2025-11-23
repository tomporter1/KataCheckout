namespace Kata.Checkout.Items;

public interface IItem
{
    public string Name { get; }
    public int CalculatePrice(int quantity) => 0;
}