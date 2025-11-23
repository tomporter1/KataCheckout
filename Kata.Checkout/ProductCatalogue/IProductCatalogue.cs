namespace Kata.Checkout.Items;

public interface IProductCatalogue
{
    IItem GetItem(string itemName);
}