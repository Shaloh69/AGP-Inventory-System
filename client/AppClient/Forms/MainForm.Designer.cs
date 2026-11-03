namespace AppClient.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        panelSidebar = new Panel();
        flowNav = new FlowLayoutPanel();
        lblLogo = new Label();
        panelHeader = new Panel();
        lblApiUrl = new Label();
        lblPageTitle = new Label();
        panelContent = new Panel();
        panelSidebar.SuspendLayout();
        panelHeader.SuspendLayout();
        SuspendLayout();
        //
        // panelSidebar
        //
        panelSidebar.BackColor = Color.FromArgb(21, 23, 41);
        panelSidebar.Controls.Add(flowNav);
        panelSidebar.Controls.Add(lblLogo);
        panelSidebar.Dock = DockStyle.Left;
        panelSidebar.Location = new Point(0, 0);
        panelSidebar.Name = "panelSidebar";
        panelSidebar.Size = new Size(240, 761);
        panelSidebar.TabIndex = 0;
        //
        // flowNav
        //
        flowNav.AutoScroll = true;
        flowNav.Dock = DockStyle.Fill;
        flowNav.FlowDirection = FlowDirection.TopDown;
        flowNav.Location = new Point(0, 72);
        flowNav.Name = "flowNav";
        flowNav.Padding = new Padding(12, 4, 0, 12);
        flowNav.Size = new Size(240, 689);
        flowNav.TabIndex = 1;
        flowNav.WrapContents = false;
        //
        // lblLogo
        //
        lblLogo.Dock = DockStyle.Top;
        lblLogo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblLogo.ForeColor = Color.White;
        lblLogo.Location = new Point(0, 0);
        lblLogo.Name = "lblLogo";
        lblLogo.Padding = new Padding(20, 0, 0, 0);
        lblLogo.Size = new Size(240, 72);
        lblLogo.TabIndex = 0;
        lblLogo.Text = "AGP Inventory";
        lblLogo.TextAlign = ContentAlignment.MiddleLeft;
        //
        // panelHeader
        //
        panelHeader.BackColor = Color.White;
        panelHeader.Controls.Add(lblApiUrl);
        panelHeader.Controls.Add(lblPageTitle);
        panelHeader.Dock = DockStyle.Top;
        panelHeader.Location = new Point(240, 0);
        panelHeader.Name = "panelHeader";
        panelHeader.Size = new Size(944, 72);
        panelHeader.TabIndex = 1;
        //
        // lblApiUrl
        //
        lblApiUrl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblApiUrl.Font = new Font("Segoe UI", 9F);
        lblApiUrl.ForeColor = Color.FromArgb(93, 98, 122);
        lblApiUrl.Location = new Point(560, 26);
        lblApiUrl.Name = "lblApiUrl";
        lblApiUrl.Size = new Size(364, 20);
        lblApiUrl.TabIndex = 1;
        lblApiUrl.Text = "API:";
        lblApiUrl.TextAlign = ContentAlignment.MiddleRight;
        //
        // lblPageTitle
        //
        lblPageTitle.AutoSize = true;
        lblPageTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblPageTitle.ForeColor = Color.FromArgb(20, 22, 38);
        lblPageTitle.Location = new Point(28, 18);
        lblPageTitle.Name = "lblPageTitle";
        lblPageTitle.Size = new Size(84, 37);
        lblPageTitle.TabIndex = 0;
        lblPageTitle.Text = "Home";
        //
        // panelContent
        //
        panelContent.BackColor = Color.FromArgb(245, 246, 250);
        panelContent.Dock = DockStyle.Fill;
        panelContent.Location = new Point(240, 72);
        panelContent.Name = "panelContent";
        panelContent.Size = new Size(944, 689);
        panelContent.TabIndex = 2;
        //
        // MainForm
        //
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1184, 761);
        Controls.Add(panelContent);
        Controls.Add(panelHeader);
        Controls.Add(panelSidebar);
        Font = new Font("Segoe UI", 10F);
        MinimumSize = new Size(1000, 680);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "AGP Inventory System";
        panelSidebar.ResumeLayout(false);
        panelHeader.ResumeLayout(false);
        panelHeader.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Panel panelSidebar;
    private FlowLayoutPanel flowNav;
    private Label lblLogo;
    private Panel panelHeader;
    private Label lblPageTitle;
    private Label lblApiUrl;
    private Panel panelContent;
}
