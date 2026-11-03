using MaterialSkin;
using MaterialSkin.Controls;

namespace AppClient.Pages.Libraries;

// UI library demo: MaterialSkin.2 (Google Material Design for WinForms).  NuGet: MaterialSkin.2
//
// TRY THIS
//   1. Change Primary.Indigo500 to Primary.Teal500 and see the controls re-colour.
//   2. Add a second MaterialButton "Clear" that empties txtName and resets lblGreeting.
//   3. Add a MaterialComboBox with categories ("Tools", "Paint", "Hardware").
//   4. Build a product form: Name, Quantity (MaterialTextBox2) and a Save button.
//   5. For a full Material window, make MainForm inherit MaterialForm instead of Form.
public class MaterialSkinPage : UserControl
{
    private readonly MaterialSkinManager skin = MaterialSkinManager.Instance;

    public MaterialSkinPage()
    {
        skin.Theme = MaterialSkinManager.Themes.LIGHT;
        skin.ColorScheme = new ColorScheme(
            Primary.Indigo500, Primary.Indigo700, Primary.Indigo100,
            Accent.Pink200, TextShade.WHITE);
        BackColor = skin.BackgroundColor;

        var card = new MaterialCard { Location = new Point(30, 24), Size = new Size(560, 300) };
        var title = new MaterialLabel
        {
            Text = "Hello, World!",
            FontType = MaterialSkinManager.fontType.H4,
            AutoSize = true,
            Location = new Point(22, 18)
        };
        var subtitle = new MaterialLabel
        {
            Text = "MaterialSkin.2  ·  WinForms  ·  Material Design",
            FontType = MaterialSkinManager.fontType.Subtitle1,
            AutoSize = true,
            Location = new Point(24, 74)
        };
        var txtName = new MaterialTextBox2 { Hint = "Your name", Location = new Point(24, 112), Width = 320 };
        var btnHello = new MaterialButton
        {
            Text = "Say hello",
            Type = MaterialButton.MaterialButtonType.Contained,
            HighEmphasis = true,
            Location = new Point(364, 118)
        };
        var lblGreeting = new MaterialLabel
        {
            Text = "Type your name and press the button.",
            AutoSize = true,
            Location = new Point(24, 186)
        };
        var swDark = new MaterialSwitch { Text = "Dark mode", AutoSize = true, Location = new Point(18, 222) };

        btnHello.Click += (_, _) =>
            lblGreeting.Text = $"Hello, {(string.IsNullOrWhiteSpace(txtName.Text) ? "World" : txtName.Text.Trim())}!";
        swDark.CheckedChanged += (_, _) =>
        {
            skin.Theme = swDark.Checked ? MaterialSkinManager.Themes.DARK : MaterialSkinManager.Themes.LIGHT;
            BackColor = skin.BackgroundColor;
            Refresh();
        };

        card.Controls.AddRange(new Control[] { title, subtitle, txtName, btnHello, lblGreeting, swDark });
        Controls.Add(card);
    }
}
