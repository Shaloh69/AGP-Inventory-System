using ReaLTaiizor.Controls;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Extension.Poison;

namespace AppClient.Pages.Libraries;

// UI library demo: ReaLTaiizor, "Poison" theme (modern Metro style).  NuGet: ReaLTaiizor
// ReaLTaiizor ships many other themes too: Hope, Material, Crown, Lost, Parrot...
//
// TRY THIS
//   1. Change ColorStyle.Teal to ColorStyle.Orange on every control.
//   2. Swap PoisonButton for a HopeButton and compare the look.
//   3. Add a PoisonGrid (data grid) and fill it with a List of fake products.
//   4. Turn this into a login form: username + password (UseSystemPasswordChar = true).
//   5. Disable btnHello while txtName is empty (txtName.TextChanged event).
public class ReaLTaiizorPage : UserControl
{
    private readonly PoisonLabel title, subtitle, lblGreeting, lblDark;
    private readonly PoisonTextBox txtName;
    private readonly PoisonButton btnHello;
    private readonly PoisonToggle tglDark;

    public ReaLTaiizorPage()
    {
        BackColor = Color.White;

        title = new PoisonLabel
        {
            Text = "Hello, World!",
            FontSize = PoisonLabelSize.Tall,
            FontWeight = PoisonLabelWeight.Bold,
            AutoSize = true,
            Location = new Point(30, 30)
        };
        subtitle = new PoisonLabel
        {
            Text = "ReaLTaiizor  ·  WinForms  ·  Poison (Metro) theme",
            AutoSize = true,
            Location = new Point(30, 72)
        };
        txtName = new PoisonTextBox
        {
            WaterMark = "Your name",
            Style = ColorStyle.Teal,
            Location = new Point(33, 114),
            Size = new Size(320, 32)
        };
        btnHello = new PoisonButton
        {
            Text = "Say hello",
            Style = ColorStyle.Teal,
            Highlight = true,
            UseStyleColors = true,
            Location = new Point(369, 114),
            Size = new Size(130, 32)
        };
        lblGreeting = new PoisonLabel
        {
            Text = "Type your name and press the button.",
            AutoSize = true,
            Location = new Point(30, 166)
        };
        tglDark = new PoisonToggle { Style = ColorStyle.Teal, DisplayStatus = false, Location = new Point(33, 208), Size = new Size(50, 20) };
        lblDark = new PoisonLabel { Text = "Dark mode", AutoSize = true, Location = new Point(91, 206) };

        btnHello.Click += (_, _) =>
            lblGreeting.Text = $"Hello, {(string.IsNullOrWhiteSpace(txtName.Text) ? "World" : txtName.Text.Trim())}!";
        tglDark.CheckedChanged += (_, _) => ApplyTheme(tglDark.Checked ? ThemeStyle.Dark : ThemeStyle.Light);

        Controls.AddRange(new Control[] { title, subtitle, txtName, btnHello, lblGreeting, tglDark, lblDark });
    }

    // Every Poison control has its own Theme property, so we update them all.
    private void ApplyTheme(ThemeStyle theme)
    {
        BackColor = theme == ThemeStyle.Dark ? Color.FromArgb(17, 17, 17) : Color.White;
        title.Theme = subtitle.Theme = lblGreeting.Theme = lblDark.Theme = theme;
        txtName.Theme = theme;
        btnHello.Theme = theme;
        tglDark.Theme = theme;
        Refresh();
    }
}
