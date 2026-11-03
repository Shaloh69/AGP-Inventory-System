using System.IO;

namespace AppClient.Helpers;

// Catches errors nobody handled and shows WHERE they happened (file, method, line),
// so "Object reference not set to an instance of an object" can actually be fixed.
// The same text is saved to %LOCALAPPDATA%\AGP Inventory\error.log - send that file when asking for help.
public static class ErrorReporter
{
    public static readonly string LogFile = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AGP Inventory", "error.log");

    public static void Install()
    {
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (_, e) => Report(e.Exception);
        AppDomain.CurrentDomain.UnhandledException += (_, e) => Report(e.ExceptionObject as Exception);
        TaskScheduler.UnobservedTaskException += (_, e) => { Report(e.Exception); e.SetObserved(); };
    }

    public static void Report(Exception? ex)
    {
        if (ex == null) return;
        string details = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}  {ex.GetType().Name}: {ex.Message}\r\n{ex}\r\n\r\n";
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(LogFile)!);
            File.AppendAllText(LogFile, details);
        }
        catch (Exception)
        {
            // could not write the log - still show the message
        }

        MessageBox.Show(
            $"{ex.Message}\r\n\r\nWhere: {FirstAppFrame(ex)}\r\n\r\nFull details were saved to:\r\n{LogFile}",
            "AGP Inventory - unexpected error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    // The first stack line that is in OUR code (not in .NET or a library) is usually the place to look.
    private static string FirstAppFrame(Exception ex)
    {
        string? line = ex.ToString().Split('\n').Select(l => l.Trim())
            .FirstOrDefault(l => l.StartsWith("at AppClient."));
        return line ?? ex.TargetSite?.ToString() ?? "unknown";
    }
}
