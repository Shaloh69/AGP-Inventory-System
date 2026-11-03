namespace AppClient.Pages;

partial class HomePage
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

    #region Component Designer generated code

    private void InitializeComponent()
    {
        lblWelcome = new Label();
        lblIntro = new Label();
        panelStatus = new Panel();
        btnCheck = new Button();
        lblDatabase = new Label();
        lblServer = new Label();
        lblStatusTitle = new Label();
        panelNext = new Panel();
        btnGoProducts = new Button();
        btnGoFunction = new Button();
        lblSteps = new Label();
        lblNextTitle = new Label();
        panelStatus.SuspendLayout();
        panelNext.SuspendLayout();
        SuspendLayout();
        //
        // lblWelcome
        //
        lblWelcome.AutoSize = true;
        lblWelcome.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        lblWelcome.ForeColor = Color.FromArgb(20, 22, 38);
        lblWelcome.Location = new Point(28, 24);
        lblWelcome.Name = "lblWelcome";
        lblWelcome.Size = new Size(434, 46);
        lblWelcome.TabIndex = 0;
        lblWelcome.Text = "Hey, you start here.";
        //
        // lblIntro
        //
        lblIntro.Font = new Font("Segoe UI", 10.5F);
        lblIntro.ForeColor = Color.FromArgb(93, 98, 122);
        lblIntro.Location = new Point(30, 78);
        lblIntro.Name = "lblIntro";
        lblIntro.Size = new Size(820, 50);
        lblIntro.TabIndex = 1;
        lblIntro.Text = "This app is your starting template. Every item in the menu is a page you can open, read and change. The pages under START HERE match the Beginner's Guide; the UI LIBRARIES pages show what each library looks like.";
        //
        // panelStatus
        //
        panelStatus.BackColor = Color.White;
        panelStatus.Controls.Add(btnCheck);
        panelStatus.Controls.Add(lblDatabase);
        panelStatus.Controls.Add(lblServer);
        panelStatus.Controls.Add(lblStatusTitle);
        panelStatus.Location = new Point(30, 144);
        panelStatus.Name = "panelStatus";
        panelStatus.Padding = new Padding(20);
        panelStatus.Size = new Size(390, 210);
        panelStatus.TabIndex = 2;
        //
        // btnCheck
        //
        btnCheck.Location = new Point(22, 150);
        btnCheck.Name = "btnCheck";
        btnCheck.Size = new Size(150, 38);
        btnCheck.TabIndex = 3;
        btnCheck.Text = "Check again";
        btnCheck.Click += btnCheck_Click;
        //
        // lblDatabase
        //
        lblDatabase.AutoSize = true;
        lblDatabase.Location = new Point(22, 104);
        lblDatabase.Name = "lblDatabase";
        lblDatabase.Size = new Size(152, 23);
        lblDatabase.TabIndex = 2;
        lblDatabase.Text = "Database: checking…";
        //
        // lblServer
        //
        lblServer.AutoSize = true;
        lblServer.Location = new Point(22, 70);
        lblServer.Name = "lblServer";
        lblServer.Size = new Size(134, 23);
        lblServer.TabIndex = 1;
        lblServer.Text = "Server: checking…";
        //
        // lblStatusTitle
        //
        lblStatusTitle.AutoSize = true;
        lblStatusTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblStatusTitle.Location = new Point(20, 20);
        lblStatusTitle.Name = "lblStatusTitle";
        lblStatusTitle.Size = new Size(115, 28);
        lblStatusTitle.TabIndex = 0;
        lblStatusTitle.Text = "Connection";
        //
        // panelNext
        //
        panelNext.BackColor = Color.White;
        panelNext.Controls.Add(btnGoProducts);
        panelNext.Controls.Add(btnGoFunction);
        panelNext.Controls.Add(lblSteps);
        panelNext.Controls.Add(lblNextTitle);
        panelNext.Location = new Point(440, 144);
        panelNext.Name = "panelNext";
        panelNext.Padding = new Padding(20);
        panelNext.Size = new Size(410, 210);
        panelNext.TabIndex = 3;
        //
        // btnGoProducts
        //
        btnGoProducts.Location = new Point(206, 150);
        btnGoProducts.Name = "btnGoProducts";
        btnGoProducts.Size = new Size(170, 38);
        btnGoProducts.TabIndex = 3;
        btnGoProducts.Text = "Open Products →";
        btnGoProducts.Click += btnGoProducts_Click;
        //
        // btnGoFunction
        //
        btnGoFunction.Location = new Point(22, 150);
        btnGoFunction.Name = "btnGoFunction";
        btnGoFunction.Size = new Size(170, 38);
        btnGoFunction.TabIndex = 2;
        btnGoFunction.Text = "First function →";
        btnGoFunction.Click += btnGoFunction_Click;
        //
        // lblSteps
        //
        lblSteps.Location = new Point(22, 58);
        lblSteps.Name = "lblSteps";
        lblSteps.Size = new Size(370, 84);
        lblSteps.TabIndex = 1;
        lblSteps.Text = "1. Open First function and change the formula.\r\n2. Open Products: data from PostgreSQL.\r\n3. Add, edit and delete a product.\r\n4. Compare the UI library pages.";
        //
        // lblNextTitle
        //
        lblNextTitle.AutoSize = true;
        lblNextTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblNextTitle.Location = new Point(20, 20);
        lblNextTitle.Name = "lblNextTitle";
        lblNextTitle.Size = new Size(112, 28);
        lblNextTitle.TabIndex = 0;
        lblNextTitle.Text = "Try this next";
        //
        // HomePage
        //
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoScroll = true;
        BackColor = Color.FromArgb(245, 246, 250);
        Controls.Add(panelNext);
        Controls.Add(panelStatus);
        Controls.Add(lblIntro);
        Controls.Add(lblWelcome);
        Font = new Font("Segoe UI", 10F);
        Name = "HomePage";
        Size = new Size(900, 600);
        panelStatus.ResumeLayout(false);
        panelStatus.PerformLayout();
        panelNext.ResumeLayout(false);
        panelNext.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblWelcome;
    private Label lblIntro;
    private Panel panelStatus;
    private Label lblStatusTitle;
    private Label lblServer;
    private Label lblDatabase;
    private Button btnCheck;
    private Panel panelNext;
    private Label lblNextTitle;
    private Label lblSteps;
    private Button btnGoFunction;
    private Button btnGoProducts;
}
