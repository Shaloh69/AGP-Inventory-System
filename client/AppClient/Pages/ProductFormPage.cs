using AppClient.Helpers;
using AppClient.Models;
using AppClient.Services;

namespace AppClient.Pages;

// One page for both "Add" and "Edit".
//   new ProductFormPage()          -> empty form, Save creates a new product (POST)
//   new ProductFormPage(product)   -> filled form, Save updates that product (PUT)
public partial class ProductFormPage : UserControl
{
    private readonly Product? editing; // null = adding a new product

    public ProductFormPage(Product? product = null)
    {
        InitializeComponent();
        Theme.StylePrimary(btnSave);
        Theme.StyleSecondary(btnCancel);

        editing = product;
        if (editing != null)
        {
            // Data passed in from ProductsPage fills the form.
            lblHeading.Text = $"Edit product #{editing.Id}";
            txtName.Text = editing.Name;
            numQuantity.Value = editing.Quantity;
            numPrice.Value = editing.Price;
        }
        UpdateTotal();
    }

    private void Inputs_Changed(object? sender, EventArgs e) => UpdateTotal();

    private void UpdateTotal()
    {
        decimal total = InventoryMath.CalculateTotal(numPrice.Value, (int)numQuantity.Value);
        lblTotal.Text = $"Total value: {total:N2}";
    }

    // ---------- STORE: send the form to the API, which saves it in PostgreSQL ----------
    private async void btnSave_Click(object? sender, EventArgs e)
    {
        lblError.Text = "";

        // 1. Check the input before calling the server
        if (string.IsNullOrWhiteSpace(txtName.Text))
        {
            lblError.Text = "Please type a product name.";
            txtName.Focus();
            return;
        }

        // 2. Copy the form into a Product object
        var product = new Product
        {
            Id = editing?.Id ?? 0,
            Name = txtName.Text.Trim(),
            Quantity = (int)numQuantity.Value,
            Price = numPrice.Value,
        };

        // 3. Save it
        btnSave.Enabled = false;
        try
        {
            if (editing == null)
                await ApiClient.CreateProductAsync(product);   // POST /api/products
            else
                await ApiClient.UpdateProductAsync(product);   // PUT  /api/products/{id}

            // 4. Go back to the list
            Navigator.Go(new ProductsPage(), "Products");
        }
        catch (ApiException ex)
        {
            lblError.Text = ex.Message; // e.g. "That name already exists"
        }
        catch (Exception ex)
        {
            lblError.Text = "Could not reach the server: " + ex.Message;
        }
        finally
        {
            if (!IsDisposed) btnSave.Enabled = true;
        }
    }

    private void btnCancel_Click(object? sender, EventArgs e) => Navigator.Go(new ProductsPage(), "Products");
}
