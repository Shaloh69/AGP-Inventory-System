using AppClient.Helpers;

namespace AppClient.Pages;

// Shows how to write and call your first function (see Helpers/InventoryMath.cs).
public partial class FirstFunctionPage : UserControl
{
    public FirstFunctionPage()
    {
        InitializeComponent();
        Theme.StylePrimary(btnCalculate);
        lblCode.Text =
            "// Helpers/InventoryMath.cs\r\n\r\n" +
            "public static decimal CalculateTotal(\r\n" +
            "    decimal price, int quantity)\r\n" +
            "{\r\n" +
            "    return price * quantity;\r\n" +
            "}\r\n\r\n" +
            "public static bool IsLowStock(int quantity)\r\n" +
            "{\r\n" +
            "    return quantity <= LowStockLimit;  // 5\r\n" +
            "}";

        // Show a result straight away using the starting values.
        btnCalculate_Click(null, EventArgs.Empty);
    }

    // Runs when the Calculate button is clicked.
    private void btnCalculate_Click(object? sender, EventArgs e)
    {
        // 1. Read the inputs
        decimal price = numPrice.Value;
        int quantity = (int)numQuantity.Value;

        // 2. Call the functions
        decimal total = InventoryMath.CalculateTotal(price, quantity);
        bool lowStock = InventoryMath.IsLowStock(quantity);

        // 3. Show the results
        lblResult.Text = $"Total: {total:N2}";
        lblStock.Text = lowStock ? "⚠ Low stock - time to re-order" : "✓ Stock level is fine";
        lblStock.ForeColor = lowStock ? Theme.Danger : Theme.Success;
    }
}
