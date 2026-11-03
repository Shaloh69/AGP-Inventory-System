using AppClient.Helpers;
using AppClient.Models;
using AppClient.Services;

namespace AppClient.Pages;

// Reads products from the database (through the API) and shows them in a grid.
// Add / Edit open ProductFormPage; Delete removes the selected row.
public partial class ProductsPage : UserControl
{
    public ProductsPage()
    {
        InitializeComponent();
        Theme.StyleSecondary(btnSearch);
        Theme.StylePrimary(btnAdd);
        Theme.StyleSecondary(btnEdit);
        Theme.StyleSecondary(btnDelete);
        btnDelete.ForeColor = Theme.Danger;
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        await LoadProductsAsync();
    }

    // ---------- READ: call the database through the API ----------
    private async Task LoadProductsAsync()
    {
        lblStatus.Text = "Loading…";
        try
        {
            List<Product> products = await ApiClient.GetProductsAsync(txtSearch.Text);
            dgvProducts.DataSource = products;
            FormatColumns();
            int low = products.Count(p => InventoryMath.IsLowStock(p.Quantity));
            lblStatus.Text = $"{products.Count} product(s) · {low} low on stock · from {ApiClient.BaseUrl}";
        }
        catch (Exception ex)
        {
            dgvProducts.DataSource = null;
            lblStatus.Text = "Could not load products: " + ex.Message;
        }
    }

    private void FormatColumns()
    {
        if (dgvProducts.Columns.Count == 0) return;
        dgvProducts.Columns[nameof(Product.Id)]!.FillWeight = 30;
        dgvProducts.Columns[nameof(Product.Name)]!.FillWeight = 160;
        dgvProducts.Columns[nameof(Product.Price)]!.DefaultCellStyle.Format = "N2";
        dgvProducts.Columns[nameof(Product.TotalValue)]!.DefaultCellStyle.Format = "N2";
        foreach (DataGridViewColumn column in dgvProducts.Columns)
            column.DefaultCellStyle.Padding = new Padding(6, 4, 6, 4);
        dgvProducts.RowTemplate.Height = 34;
    }

    // Paint low-stock rows orange, using the same function as the First function page.
    private void dgvProducts_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (dgvProducts.Rows[e.RowIndex].DataBoundItem is Product product && InventoryMath.IsLowStock(product.Quantity))
            e.CellStyle!.BackColor = Theme.Warning;
    }

    private Product? SelectedProduct => dgvProducts.CurrentRow?.DataBoundItem as Product;

    private async void btnSearch_Click(object? sender, EventArgs e) => await LoadProductsAsync();

    private async void txtSearch_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter) return;
        e.SuppressKeyPress = true;
        await LoadProductsAsync();
    }

    // ---------- open another page, passing data to it ----------
    private void btnAdd_Click(object? sender, EventArgs e) =>
        Navigator.Go(new ProductFormPage(), "Add product");

    private void btnEdit_Click(object? sender, EventArgs e)
    {
        if (SelectedProduct is not Product product)
        {
            MessageBox.Show("Select a product first.", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        Navigator.Go(new ProductFormPage(product), "Edit product");
    }

    private void dgvProducts_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0) btnEdit_Click(sender, e);
    }

    // ---------- DELETE ----------
    private async void btnDelete_Click(object? sender, EventArgs e)
    {
        if (SelectedProduct is not Product product) return;

        var answer = MessageBox.Show($"Delete \"{product.Name}\"?", "Delete product",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (answer != DialogResult.Yes) return;

        try
        {
            await ApiClient.DeleteProductAsync(product.Id);
            await LoadProductsAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Could not delete: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
