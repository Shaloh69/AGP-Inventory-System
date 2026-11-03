using AppClient.Helpers;
using AppClient.Pages;
using AppClient.Pages.Libraries;
using AppClient.Services;
using AppClient.WpfPages;

namespace AppClient.Forms;

// The main window ("shell"): a sidebar menu on the left and the current page on the right.
public partial class MainForm : Form
{
    // ★ Every page in the sidebar.
    //   To add a page: create a UserControl in Pages/, then add ONE line here.
    private readonly List<MenuItem> menu =
    [
        MenuItem.Section("START HERE"),
        new("Home", () => new HomePage()),
        new("First function", () => new FirstFunctionPage()),
        new("Products", () => new ProductsPage()),

        MenuItem.Section("UI LIBRARIES · WINFORMS"),
        new("MaterialSkin.2", () => new MaterialSkinPage()),
        new("ReaLTaiizor", () => new ReaLTaiizorPage()),
        new("Krypton", () => new KryptonPage()),

        MenuItem.Section("UI LIBRARIES · WPF"),
        new("MaterialDesign", () => new WpfHostPage(new MaterialDesignPage())),
        new("WPF UI", () => new WpfHostPage(new WpfUiPage())),
        new("MahApps", () => new WpfHostPage(new MahAppsPage())),
        new("HandyControl", () => new WpfHostPage(new HandyControlPage())),
    ];

    private readonly Dictionary<string, Button> menuButtons = new();
    private readonly string? startPage;

    public MainForm(string? startPage = null)
    {
        InitializeComponent();
        this.startPage = startPage;
        Navigator.Attach(this);
        lblApiUrl.Text = "API: " + ApiClient.BaseUrl;
        BuildMenu();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        // Open the page named on the command line (--page Products), or Home.
        var first = menu.FirstOrDefault(m => m.Create != null && startPage != null &&
                        Simplify(m.Title).Contains(Simplify(startPage)))
                    ?? menu.First(m => m.Create != null);
        OpenMenuItem(first);
    }

    // Shows a page in the content area. Called by the menu and by Navigator.Go(...).
    public void ShowPage(UserControl page, string title)
    {
        foreach (Control old in panelContent.Controls.Cast<Control>().ToList())
        {
            panelContent.Controls.Remove(old);
            old.Dispose();
        }

        page.Dock = DockStyle.Fill;
        panelContent.Controls.Add(page);
        lblPageTitle.Text = title;
        HighlightMenu(title);
    }

    private void BuildMenu()
    {
        foreach (var item in menu)
        {
            if (item.Create == null)
            {
                flowNav.Controls.Add(new Label
                {
                    Text = item.Title,
                    AutoSize = false,
                    Size = new Size(210, 34),
                    TextAlign = ContentAlignment.BottomLeft,
                    ForeColor = Color.FromArgb(130, 136, 170),
                    Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                    Margin = new Padding(8, 10, 0, 4),
                });
                continue;
            }

            var button = new Button
            {
                Text = item.Title,
                Size = new Size(212, 38),
                TextAlign = ContentAlignment.MiddleLeft,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(214, 218, 240),
                BackColor = Theme.Sidebar,
                Font = new Font("Segoe UI", 10F),
                Padding = new Padding(10, 0, 0, 0),
                Margin = new Padding(0, 1, 0, 1),
                Cursor = Cursors.Hand,
            };
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Theme.SidebarHover;
            button.Click += (_, _) => OpenMenuItem(item);
            menuButtons[item.Title] = button;
            flowNav.Controls.Add(button);
        }
    }

    private void OpenMenuItem(MenuItem item) => ShowPage(item.Create!(), item.Title);

    private void HighlightMenu(string title)
    {
        foreach (var (name, button) in menuButtons)
        {
            bool active = name == title;
            button.BackColor = active ? Theme.Accent : Theme.Sidebar;
            button.ForeColor = active ? Color.White : Color.FromArgb(214, 218, 240);
        }
    }

    private static string Simplify(string text) => text.Replace(" ", "").Replace(".", "").ToLowerInvariant();

    // One row of the sidebar: a title and a function that creates the page (null = section heading).
    private record MenuItem(string Title, Func<UserControl>? Create)
    {
        public static MenuItem Section(string title) => new(title, null);
    }
}
