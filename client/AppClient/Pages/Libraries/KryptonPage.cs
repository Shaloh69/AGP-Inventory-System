using Krypton.Toolkit;

namespace AppClient.Pages.Libraries;

// UI library demo: Krypton Toolkit (Office / Microsoft 365 style).  NuGet: Krypton.Toolkit
//
// TRY THIS
//   1. Try other palettes: PaletteMode.Office2010Silver, SparklePurple, MaterialLight.
//   2. Add a KryptonDataGridView docked to the bottom with fake inventory rows.
//   3. Add a KryptonComboBox that lets the user pick the palette at runtime.
//   4. Replace the CheckBox with a KryptonToggleSwitch.
//   5. Show KryptonMessageBox.Show("Saved!") when the button is clicked.
public class KryptonPage : UserControl
{
    // KryptonManager sets the palette (colour theme) of every Krypton control.
    private readonly KryptonManager manager = new() { GlobalPaletteMode = PaletteMode.Microsoft365Blue };

    public KryptonPage()
    {
        var panel = new KryptonPanel { Dock = DockStyle.Fill };

        var title = new KryptonLabel { LabelStyle = LabelStyle.TitlePanel, AutoSize = true, Location = new Point(28, 24) };
        title.Values.Text = "Hello, World!";
        title.StateCommon.ShortText.Font = new Font("Segoe UI", 26F, FontStyle.Bold);

        var subtitle = new KryptonLabel { AutoSize = true, Location = new Point(32, 84) };
        subtitle.Values.Text = "Krypton Toolkit  ·  WinForms  ·  Microsoft 365 palette";

        var txtName = new KryptonTextBox { Location = new Point(34, 128), Width = 320 };
        txtName.CueHint.CueHintText = "Your name";

        var btnHello = new KryptonButton { Location = new Point(370, 126), Size = new Size(130, 30) };
        btnHello.Values.Text = "Say hello";

        var lblGreeting = new KryptonLabel { AutoSize = true, Location = new Point(32, 180) };
        lblGreeting.Values.Text = "Type your name and press the button.";

        var chkDark = new KryptonCheckBox { Location = new Point(34, 220) };
        chkDark.Values.Text = "Dark mode";

        btnHello.Click += (_, _) =>
            lblGreeting.Values.Text = $"Hello, {(string.IsNullOrWhiteSpace(txtName.Text) ? "World" : txtName.Text.Trim())}!";
        chkDark.CheckedChanged += (_, _) =>
            manager.GlobalPaletteMode = chkDark.Checked ? PaletteMode.Microsoft365BlackDarkMode : PaletteMode.Microsoft365Blue;

        panel.Controls.AddRange(new Control[] { title, subtitle, txtName, btnHello, lblGreeting, chkDark });
        Controls.Add(panel);
    }
}
