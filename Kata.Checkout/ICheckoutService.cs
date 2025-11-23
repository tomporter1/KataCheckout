namespace Kata.Checkout;

public interface ICheckoutService
{
    void ScanItem(string item);
    void RemoveItem(string item);
    float Total();
}