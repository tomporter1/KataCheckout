namespace Kata.Checkout.Items;

public class ProductCatalogue(Dictionary<string, IItem> items) : IProductCatalogue
{
    public IItem GetItem(string itemName)
    {
        if (items.TryGetValue(itemName, out IItem item))
            return item;
        throw new KeyNotFoundException($"Item {itemName} not found in catalogue");
    }
}