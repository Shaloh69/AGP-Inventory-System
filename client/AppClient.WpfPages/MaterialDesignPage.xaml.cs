using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace AppClient.WpfPages;

// UI library demo: MaterialDesignInXamlToolkit.  NuGet: MaterialDesignThemes
//
// TRY THIS (the layout is in MaterialDesignPage.xaml)
//   1. Change PrimaryColor="DeepPurple" to "Teal" or "Indigo".
//   2. Change PackIcon Kind="HandWave" to "Package" or "Warehouse" (any Material icon name).
//   3. Add a DataGrid under the card and fill it with a List of fake products.
//   4. Wrap the content in materialDesign:DialogHost and show a "Saved!" dialog.
//   5. Add a second Card side by side using a UniformGrid Columns="2".
public partial class MaterialDesignPage : UserControl
{
    public MaterialDesignPage()
    {
        InitializeComponent();
    }

    private void SayHello_Click(object sender, RoutedEventArgs e)
    {
        var name = string.IsNullOrWhiteSpace(txtName.Text) ? "World" : txtName.Text.Trim();
        lblGreeting.Text = $"Hello, {name}!";
    }

    // The theme lives in this page's Resources, so change it there.
    private void Dark_Click(object sender, RoutedEventArgs e)
    {
        var theme = Resources.GetTheme();
        theme.SetBaseTheme(tglDark.IsChecked == true ? BaseTheme.Dark : BaseTheme.Light);
        Resources.SetTheme(theme);
    }
}
