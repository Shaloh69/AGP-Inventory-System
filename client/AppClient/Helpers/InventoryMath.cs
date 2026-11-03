namespace AppClient.Helpers;

// Your first functions live here. They take values in and give a result back,
// and they do not touch the screen or the internet - so they are easy to test and reuse.
public static class InventoryMath
{
    public const int LowStockLimit = 5;

    // Total value of the stock on hand: price x quantity.
    public static decimal CalculateTotal(decimal price, int quantity)
    {
        return price * quantity;
    }

    // true when the item should be re-ordered soon.
    public static bool IsLowStock(int quantity)
    {
        return quantity <= LowStockLimit;
    }
}
