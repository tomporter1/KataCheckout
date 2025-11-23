using Kata.Checkout.Items;

namespace Kata.Checkout;

public class CheckoutService(IProductCatalogue products) : ICheckoutService
{
    private readonly Dictionary<string, int> _items = new();
    private readonly IProductCatalogue _products = products;

    public void ScanItem(string item)
    {
        if (!_items.TryAdd(item, 1))
        {
            _items[item]++;
        }
    }

    public void RemoveItem(string item)
    {
        if (!_items.TryGetValue(item, out int quantity))
            return;

        if (quantity > 1)
            _items[item] = --quantity;
        else
            _items.Remove(item);
    }

    public int Total()
    {
        int total = 0;
        foreach ((string itemName, int quantity) in _items.Where(i => _products.Contains(i.Key)))
        {
            IItem item = _products.GetItem(itemName);
            total += item.CalculatePrice(quantity);
        }
        return total;
    }
}