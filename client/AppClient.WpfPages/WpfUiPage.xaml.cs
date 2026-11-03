using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Appearance;
using Wpf.Ui.Markup;

namespace AppClient.WpfPages;

// UI library demo: WPF UI (lepoco).  NuGet: WPF-UI
//
// TRY THIS (the layout is in WpfUiPage.xaml)
//   1. Change the accent colour #5B4BFF in the XAML resources to your own brand colour.
//   2. Change the icon Symbol="HandWave24" to "Box24" or "ShoppingBag24".
//   3. Add a ui:InfoBar (Severity="Success") that appears after "Say hello".
//   4. Add a ui:NumberBox for quantity next to the name box.
//   5. For a full Fluent window with Mica, build a WPF app with ui:FluentWindow.
public partial class WpfUiPage : UserControl
{
    public WpfUiPage()
    {
        InitializeComponent();
    }

    private void SayHello_Click(object sender, RoutedEventArgs e)
    {
        var name = string.IsNullOrWhiteSpace(txtName.Text) ? "World" : txtName.Text.Trim();
        lblGreeting.Text = $"Hello, {name}!";
    }

    // Swap the colour dictionary (index 0 in the XAML) for the light or dark one.
    private void Dark_Click(object sender, RoutedEventArgs e)
    {
        var theme = tglDark.IsChecked == true ? ApplicationTheme.Dark : ApplicationTheme.Light;
        Resources.MergedDictionaries[0] = new ThemesDictionary { Theme = theme };
    }
}
