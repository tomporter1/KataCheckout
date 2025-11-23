namespace Kata.Checkout.Items;

public interface IItem
{
    public string Name { get; }
    public float CalculatePrice(int quantity) => 0;
}