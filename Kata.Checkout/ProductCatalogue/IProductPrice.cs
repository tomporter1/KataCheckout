namespace Kata.Checkout.Items;

public interface IProductPrice
{
    public int CalculatePrice(int quantity) => 0;
}