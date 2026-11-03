namespace AppClient.Pages;

partial class ProductsPage
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
        panelToolbar = new Panel();
        btnDelete = new Button();
        btnEdit = new Button();
        btnAdd = new Button();
        btnSearch = new Button();
        txtSearch = new TextBox();
        dgvProducts = new DataGridView();
        lblStatus = new Label();
        panelToolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
        SuspendLayout();
        //
        // panelToolbar
        //
        panelToolbar.Controls.Add(btnDelete);
        panelToolbar.Controls.Add(btnEdit);
        panelToolbar.Controls.Add(btnAdd);
        panelToolbar.Controls.Add(btnSearch);
        panelToolbar.Controls.Add(txtSearch);
        panelToolbar.Dock = DockStyle.Top;
        panelToolbar.Location = new Point(24, 20);
        panelToolbar.Name = "panelToolbar";
        panelToolbar.Size = new Size(852, 56);
        panelToolbar.TabIndex = 0;
        //
        // btnDelete
        //
        btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnDelete.Location = new Point(752, 6);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(100, 38);
        btnDelete.TabIndex = 4;
        btnDelete.Text = "Delete";
        btnDelete.Click += btnDelete_Click;
        //
        // btnEdit
        //
        btnEdit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnEdit.Location = new Point(644, 6);
        btnEdit.Name = "btnEdit";
        btnEdit.Size = new Size(100, 38);
        btnEdit.TabIndex = 3;
        btnEdit.Text = "Edit";
        btnEdit.Click += btnEdit_Click;
        //
        // btnAdd
        //
        btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAdd.Location = new Point(486, 6);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(150, 38);
        btnAdd.TabIndex = 2;
        btnAdd.Text = "+ Add product";
        btnAdd.Click += btnAdd_Click;
        //
        // btnSearch
        //
        btnSearch.Location = new Point(270, 6);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(100, 38);
        btnSearch.TabIndex = 1;
        btnSearch.Text = "Search";
        btnSearch.Click += btnSearch_Click;
        //
        // txtSearch
        //
        txtSearch.Location = new Point(0, 10);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "Search by name…";
        txtSearch.Size = new Size(260, 30);
        txtSearch.TabIndex = 0;
        txtSearch.KeyDown += txtSearch_KeyDown;
        //
        // dgvProducts
        //
        dgvProducts.AllowUserToAddRows = false;
        dgvProducts.AllowUserToDeleteRows = false;
        dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvProducts.BackgroundColor = Color.White;
        dgvProducts.BorderStyle = BorderStyle.None;
        dgvProducts.Dock = DockStyle.Fill;
        dgvProducts.Location = new Point(24, 76);
        dgvProducts.MultiSelect = false;
        dgvProducts.Name = "dgvProducts";
        dgvProducts.ReadOnly = true;
        dgvProducts.RowHeadersVisible = false;
        dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvProducts.Size = new Size(852, 472);
        dgvProducts.TabIndex = 1;
        dgvProducts.CellDoubleClick += dgvProducts_CellDoubleClick;
        dgvProducts.CellFormatting += dgvProducts_CellFormatting;
        //
        // lblStatus
        //
        lblStatus.Dock = DockStyle.Bottom;
        lblStatus.ForeColor = Color.FromArgb(93, 98, 122);
        lblStatus.Location = new Point(24, 548);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(852, 32);
        lblStatus.TabIndex = 2;
        lblStatus.Text = "Loading…";
        lblStatus.TextAlign = ContentAlignment.MiddleLeft;
        //
        // ProductsPage
        //
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 246, 250);
        Controls.Add(dgvProducts);
        Controls.Add(lblStatus);
        Controls.Add(panelToolbar);
        Font = new Font("Segoe UI", 10F);
        Name = "ProductsPage";
        Padding = new Padding(24, 20, 24, 20);
        Size = new Size(900, 600);
        panelToolbar.ResumeLayout(false);
        panelToolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel panelToolbar;
    private TextBox txtSearch;
    private Button btnSearch;
    private Button btnAdd;
    private Button btnEdit;
    private Button btnDelete;
    private DataGridView dgvProducts;
    private Label lblStatus;
}
