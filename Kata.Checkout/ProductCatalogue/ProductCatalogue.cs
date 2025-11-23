namespace Kata.Checkout.Items;

public class ProductCatalogue(Dictionary<string, IProductPrice> items) : IProductCatalogue
{
    public bool Contains(string itemName) => items.ContainsKey(itemName);

    public IProductPrice GetItem(string itemName)
    {
        if (items.TryGetValue(itemName, out IProductPrice item))
            return item;
        throw new KeyNotFoundException($"ProductPrice {itemName} not found in catalogue");
    }
}