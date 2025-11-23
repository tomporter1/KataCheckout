using Kata.Checkout.Items;

namespace Kata.Checkout;

public class CheckoutService(IProductCatalogue products) : ICheckoutService
{
    private readonly Dictionary<string, int> _items = new();
    private readonly IProductCatalogue _products = products;

    public void ScanItem(string item)
    {
        if (_items.ContainsKey(item))
        {
            _items[item]++;
        }
        else
        {
            _items[item] = 1;
        }
    }

    public void RemoveItem(string item)
    {
        if (!_items.ContainsKey(item))
            return;

        if (_items[item] > 1)
            _items[item]--;
        else
            _items.Remove(item);
    }

    public int Total()
    {
        int total = 0;
        foreach ((string itemName, int quantity) in _items)
        {
            IItem item = _products.GetItem(itemName);
            total += item.CalculatePrice(quantity);
        }
        return total;
    }
}