using System.Windows.Forms.Integration;

namespace AppClient.Pages.Libraries;

// Shows a WPF page (from the AppClient.WpfPages project) inside this WinForms app.
// ElementHost is the bridge between WinForms and WPF.
public class WpfHostPage : UserControl
{
    public WpfHostPage(System.Windows.UIElement wpfPage)
    {
        Controls.Add(new ElementHost { Dock = DockStyle.Fill, Child = wpfPage });
    }
}
