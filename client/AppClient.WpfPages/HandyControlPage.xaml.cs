using System.Windows;
using System.Windows.Controls;

namespace AppClient.WpfPages;

// UI library demo: HandyControl.  NuGet: HandyControl
//
// TRY THIS (the layout is in HandyControlPage.xaml)
//   1. Change the Button style to ButtonSuccess, ButtonWarning or ButtonDanger.
//   2. Try the "SkinViolet" skin in Dark_Click.
//   3. Add an hc:NumericUpDown for quantity and an hc:ComboBox for category.
//   4. Add an hc:SearchBar above a DataGrid of fake products.
//   5. Add an hc:Rate (star rating) or hc:Pagination control.
public partial class HandyControlPage : UserControl
{
    public HandyControlPage()
    {
        InitializeComponent();
    }

    private void SayHello_Click(object sender, RoutedEventArgs e)
    {
        var name = string.IsNullOrWhiteSpace(txtName.Text) ? "World" : txtName.Text.Trim();
        lblGreeting.Text = $"Hello, {name}!";
    }

    // Swap the skin dictionary (index 0) and reload the control styles (index 1).
    private void Dark_Click(object sender, RoutedEventArgs e)
    {
        var skin = tglDark.IsChecked == true ? "SkinDark" : "SkinDefault";
        Resources.MergedDictionaries[0] = new ResourceDictionary { Source = new Uri($"pack://application:,,,/HandyControl;component/Themes/{skin}.xaml") };
        Resources.MergedDictionaries[1] = new ResourceDictionary { Source = new Uri("pack://application:,,,/HandyControl;component/Themes/Theme.xaml") };
    }
}
