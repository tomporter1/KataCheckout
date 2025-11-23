namespace Kata.Checkout.Items;

public interface IProductCatalogue
{
    IProductPrice GetItem(string itemName);
    bool Contains(string itemName);
}