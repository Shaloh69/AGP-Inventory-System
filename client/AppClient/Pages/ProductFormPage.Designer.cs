namespace AppClient.Pages;

partial class ProductFormPage
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
        panelCard = new Panel();
        lblError = new Label();
        btnCancel = new Button();
        btnSave = new Button();
        lblTotal = new Label();
        numPrice = new NumericUpDown();
        lblPrice = new Label();
        numQuantity = new NumericUpDown();
        lblQuantity = new Label();
        txtName = new TextBox();
        lblName = new Label();
        lblHeading = new Label();
        panelCard.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
        SuspendLayout();
        //
        // panelCard
        //
        panelCard.BackColor = Color.White;
        panelCard.Controls.Add(lblError);
        panelCard.Controls.Add(btnCancel);
        panelCard.Controls.Add(btnSave);
        panelCard.Controls.Add(lblTotal);
        panelCard.Controls.Add(numPrice);
        panelCard.Controls.Add(lblPrice);
        panelCard.Controls.Add(numQuantity);
        panelCard.Controls.Add(lblQuantity);
        panelCard.Controls.Add(txtName);
        panelCard.Controls.Add(lblName);
        panelCard.Controls.Add(lblHeading);
        panelCard.Location = new Point(30, 24);
        panelCard.Name = "panelCard";
        panelCard.Size = new Size(520, 430);
        panelCard.TabIndex = 0;
        //
        // lblError
        //
        lblError.ForeColor = Color.FromArgb(214, 48, 49);
        lblError.Location = new Point(28, 330);
        lblError.Name = "lblError";
        lblError.Size = new Size(464, 44);
        lblError.TabIndex = 10;
        //
        // btnCancel
        //
        btnCancel.Location = new Point(176, 274);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(130, 42);
        btnCancel.TabIndex = 9;
        btnCancel.Text = "Cancel";
        btnCancel.Click += btnCancel_Click;
        //
        // btnSave
        //
        btnSave.Location = new Point(28, 274);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(140, 42);
        btnSave.TabIndex = 8;
        btnSave.Text = "Save";
        btnSave.Click += btnSave_Click;
        //
        // lblTotal
        //
        lblTotal.AutoSize = true;
        lblTotal.ForeColor = Color.FromArgb(93, 98, 122);
        lblTotal.Location = new Point(28, 230);
        lblTotal.Name = "lblTotal";
        lblTotal.Size = new Size(134, 23);
        lblTotal.TabIndex = 7;
        lblTotal.Text = "Total value: 0.00";
        //
        // numPrice
        //
        numPrice.DecimalPlaces = 2;
        numPrice.Location = new Point(264, 184);
        numPrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        numPrice.Name = "numPrice";
        numPrice.Size = new Size(228, 30);
        numPrice.TabIndex = 6;
        numPrice.ValueChanged += Inputs_Changed;
        //
        // lblPrice
        //
        lblPrice.AutoSize = true;
        lblPrice.Location = new Point(264, 156);
        lblPrice.Name = "lblPrice";
        lblPrice.Size = new Size(46, 23);
        lblPrice.TabIndex = 5;
        lblPrice.Text = "Price";
        //
        // numQuantity
        //
        numQuantity.Location = new Point(28, 184);
        numQuantity.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
        numQuantity.Name = "numQuantity";
        numQuantity.Size = new Size(220, 30);
        numQuantity.TabIndex = 4;
        numQuantity.ValueChanged += Inputs_Changed;
        //
        // lblQuantity
        //
        lblQuantity.AutoSize = true;
        lblQuantity.Location = new Point(28, 156);
        lblQuantity.Name = "lblQuantity";
        lblQuantity.Size = new Size(72, 23);
        lblQuantity.TabIndex = 3;
        lblQuantity.Text = "Quantity";
        //
        // txtName
        //
        txtName.Location = new Point(28, 106);
        txtName.MaxLength = 120;
        txtName.Name = "txtName";
        txtName.PlaceholderText = "e.g. Claw Hammer";
        txtName.Size = new Size(464, 30);
        txtName.TabIndex = 2;
        //
        // lblName
        //
        lblName.AutoSize = true;
        lblName.Location = new Point(28, 78);
        lblName.Name = "lblName";
        lblName.Size = new Size(116, 23);
        lblName.TabIndex = 1;
        lblName.Text = "Product name";
        //
        // lblHeading
        //
        lblHeading.AutoSize = true;
        lblHeading.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblHeading.Location = new Point(24, 22);
        lblHeading.Name = "lblHeading";
        lblHeading.Size = new Size(161, 32);
        lblHeading.TabIndex = 0;
        lblHeading.Text = "New product";
        //
        // ProductFormPage
        //
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoScroll = true;
        BackColor = Color.FromArgb(245, 246, 250);
        Controls.Add(panelCard);
        Font = new Font("Segoe UI", 10F);
        Name = "ProductFormPage";
        Size = new Size(900, 600);
        panelCard.ResumeLayout(false);
        panelCard.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
        ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel panelCard;
    private Label lblHeading;
    private Label lblName;
    private TextBox txtName;
    private Label lblQuantity;
    private NumericUpDown numQuantity;
    private Label lblPrice;
    private NumericUpDown numPrice;
    private Label lblTotal;
    private Button btnSave;
    private Button btnCancel;
    private Label lblError;
}
