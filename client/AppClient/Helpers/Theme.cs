namespace AppClient.Helpers;

// One place for the app's colours and fonts, so every page looks the same.
public static class Theme
{
    public static readonly Color Sidebar = Color.FromArgb(21, 23, 41);
    public static readonly Color SidebarHover = Color.FromArgb(34, 37, 62);
    public static readonly Color Accent = Color.FromArgb(91, 75, 255);
    public static readonly Color Background = Color.FromArgb(245, 246, 250);
    public static readonly Color Surface = Color.White;
    public static readonly Color Text = Color.FromArgb(20, 22, 38);
    public static readonly Color MutedText = Color.FromArgb(93, 98, 122);
    public static readonly Color Success = Color.FromArgb(24, 160, 88);
    public static readonly Color Danger = Color.FromArgb(214, 48, 49);
    public static readonly Color Warning = Color.FromArgb(255, 244, 224);

    public static readonly Font Title = new("Segoe UI", 20F, FontStyle.Bold);
    public static readonly Font Body = new("Segoe UI", 10F);
    public static readonly Font BodyBold = new("Segoe UI", 10F, FontStyle.Bold);

    // Gives a normal WinForms button the app's "primary" look.
    public static void StylePrimary(Button button)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderSize = 0;
        button.BackColor = Accent;
        button.ForeColor = Color.White;
        button.Font = BodyBold;
        button.Cursor = Cursors.Hand;
    }

    public static void StyleSecondary(Button button)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderColor = Color.FromArgb(220, 222, 232);
        button.BackColor = Surface;
        button.ForeColor = Text;
        button.Font = Body;
        button.Cursor = Cursors.Hand;
    }
}
