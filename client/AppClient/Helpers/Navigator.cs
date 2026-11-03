using AppClient.Forms;

namespace AppClient.Helpers;

// Lets any page open another page:   Navigator.Go(new ProductsPage(), "Products");
public static class Navigator
{
    private static MainForm? shell;

    // Called once by MainForm when the app starts.
    public static void Attach(MainForm mainForm) => shell = mainForm;

    public static void Go(UserControl page, string title)
    {
        if (shell == null) throw new InvalidOperationException("MainForm has not started yet.");
        shell.ShowPage(page, title);
    }
}
