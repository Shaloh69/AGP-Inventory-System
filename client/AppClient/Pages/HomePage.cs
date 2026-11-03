using AppClient.Helpers;
using AppClient.Services;

namespace AppClient.Pages;

// Home: checks the connection to the server and database, and links to the next pages.
public partial class HomePage : UserControl
{
    public HomePage()
    {
        InitializeComponent();
        Theme.StyleSecondary(btnCheck);
        Theme.StylePrimary(btnGoFunction);
        Theme.StylePrimary(btnGoProducts);
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        await CheckConnectionAsync();
    }

    private async Task CheckConnectionAsync()
    {
        btnCheck.Enabled = false;
        lblServer.Text = "Server: checking…";
        lblDatabase.Text = "Database: checking…";
        lblServer.ForeColor = lblDatabase.ForeColor = Theme.MutedText;

        bool serverUp = await ApiClient.IsServerUpAsync();
        ShowStatus(lblServer, "Server", serverUp, "not reachable - run run-dev.bat server");

        bool databaseUp = serverUp && await ApiClient.IsDatabaseUpAsync();
        ShowStatus(lblDatabase, "Database", databaseUp, serverUp ? "not reachable - check server/.env" : "unknown (server is down)");

        btnCheck.Enabled = true;
    }

    private static void ShowStatus(Label label, string name, bool ok, string problem)
    {
        label.Text = ok ? $"{name}: connected ✓" : $"{name}: {problem}";
        label.ForeColor = ok ? Theme.Success : Theme.Danger;
    }

    private async void btnCheck_Click(object? sender, EventArgs e) => await CheckConnectionAsync();

    // How to open (call) another page: create it and pass it to Navigator.Go
    private void btnGoFunction_Click(object? sender, EventArgs e) => Navigator.Go(new FirstFunctionPage(), "First function");

    private void btnGoProducts_Click(object? sender, EventArgs e) => Navigator.Go(new ProductsPage(), "Products");
}
