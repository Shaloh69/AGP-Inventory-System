using AppClient.Forms;

namespace AppClient;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    ///  Optional: start on a specific page, e.g.  AppClient.exe --page Products
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();

        // The WPF library pages (MaterialDesign, WPF UI, MahApps, HandyControl) expect a WPF
        // Application object to exist, even though this is a WinForms app.
        _ = new System.Windows.Application { ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown };

        string? startPage = args.Length >= 2 && args[0] == "--page" ? args[1] : null;
        Application.Run(new MainForm(startPage));
    }
}
