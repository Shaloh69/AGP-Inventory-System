using System.Windows;
using System.Windows.Controls;
using ControlzEx.Theming;

namespace AppClient.WpfPages;

// UI library demo: MahApps.Metro.  NuGet: MahApps.Metro
//
// TRY THIS (the layout is in MahAppsPage.xaml)
//   1. Change Light.Blue.xaml to Light.Green.xaml or Dark.Purple.xaml.
//   2. Add mah:Tile buttons ("Products", "Stock In", "Reports") as a dashboard.
//   3. Add a mah:NumericUpDown for quantity.
//   4. Add a mah:ProgressRing that shows while "loading".
//   5. For dialogs and flyouts, build a WPF app whose window is a mah:MetroWindow.
public partial class MahAppsPage : UserControl
{
    public MahAppsPage()
    {
        InitializeComponent();
    }

    private void SayHello_Click(object sender, RoutedEventArgs e)
    {
        var name = string.IsNullOrWhiteSpace(txtName.Text) ? "World" : txtName.Text.Trim();
        lblGreeting.Text = $"Hello, {name}!";
    }

    // Changes the theme of THIS page only (not the whole app).
    private void Dark_Toggled(object sender, RoutedEventArgs e)
    {
        ThemeManager.Current.ChangeTheme(this, tglDark.IsOn ? "Dark.Blue" : "Light.Blue");
    }
}
