namespace AppClient.Pages;

partial class FirstFunctionPage
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
        lblExplain = new Label();
        lblTryIt = new Label();
        panelCard = new Panel();
        lblStock = new Label();
        lblResult = new Label();
        btnCalculate = new Button();
        numQuantity = new NumericUpDown();
        lblQuantity = new Label();
        numPrice = new NumericUpDown();
        lblPrice = new Label();
        lblCode = new Label();
        panelCard.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
        SuspendLayout();
        //
        // lblExplain
        //
        lblExplain.Font = new Font("Segoe UI", 10.5F);
        lblExplain.ForeColor = Color.FromArgb(93, 98, 122);
        lblExplain.Location = new Point(30, 24);
        lblExplain.Name = "lblExplain";
        lblExplain.Size = new Size(820, 48);
        lblExplain.TabIndex = 0;
        lblExplain.Text = "A function takes values in and gives a result back. Type a price and quantity, press Calculate, then open Helpers/InventoryMath.cs and change the code to see the result change.";
        //
        // panelCard
        //
        panelCard.BackColor = Color.White;
        panelCard.Controls.Add(lblTryIt);
        panelCard.Controls.Add(lblStock);
        panelCard.Controls.Add(lblResult);
        panelCard.Controls.Add(btnCalculate);
        panelCard.Controls.Add(numQuantity);
        panelCard.Controls.Add(lblQuantity);
        panelCard.Controls.Add(numPrice);
        panelCard.Controls.Add(lblPrice);
        panelCard.Location = new Point(30, 84);
        panelCard.Name = "panelCard";
        panelCard.Size = new Size(380, 300);
        panelCard.TabIndex = 1;
        //
        // lblTryIt
        //
        lblTryIt.AutoSize = true;
        lblTryIt.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblTryIt.Location = new Point(22, 20);
        lblTryIt.Name = "lblTryIt";
        lblTryIt.Size = new Size(61, 28);
        lblTryIt.TabIndex = 7;
        lblTryIt.Text = "Try it";
        //
        // lblStock
        //
        lblStock.AutoSize = true;
        lblStock.Location = new Point(24, 252);
        lblStock.Name = "lblStock";
        lblStock.Size = new Size(0, 23);
        lblStock.TabIndex = 6;
        //
        // lblResult
        //
        lblResult.AutoSize = true;
        lblResult.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblResult.ForeColor = Color.FromArgb(91, 75, 255);
        lblResult.Location = new Point(22, 206);
        lblResult.Name = "lblResult";
        lblResult.Size = new Size(176, 37);
        lblResult.TabIndex = 5;
        lblResult.Text = "Total: 0.00";
        //
        // btnCalculate
        //
        btnCalculate.Location = new Point(24, 148);
        btnCalculate.Name = "btnCalculate";
        btnCalculate.Size = new Size(160, 40);
        btnCalculate.TabIndex = 4;
        btnCalculate.Text = "Calculate";
        btnCalculate.Click += btnCalculate_Click;
        //
        // numQuantity
        //
        numQuantity.Location = new Point(196, 96);
        numQuantity.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
        numQuantity.Name = "numQuantity";
        numQuantity.Size = new Size(150, 30);
        numQuantity.TabIndex = 3;
        numQuantity.Value = new decimal(new int[] { 4, 0, 0, 0 });
        //
        // lblQuantity
        //
        lblQuantity.AutoSize = true;
        lblQuantity.Location = new Point(196, 66);
        lblQuantity.Name = "lblQuantity";
        lblQuantity.Size = new Size(72, 23);
        lblQuantity.TabIndex = 2;
        lblQuantity.Text = "Quantity";
        //
        // numPrice
        //
        numPrice.DecimalPlaces = 2;
        numPrice.Location = new Point(24, 96);
        numPrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        numPrice.Name = "numPrice";
        numPrice.Size = new Size(150, 30);
        numPrice.TabIndex = 1;
        numPrice.Value = new decimal(new int[] { 34900, 0, 0, 131072 });
        //
        // lblPrice
        //
        lblPrice.AutoSize = true;
        lblPrice.Location = new Point(24, 66);
        lblPrice.Name = "lblPrice";
        lblPrice.Size = new Size(46, 23);
        lblPrice.TabIndex = 0;
        lblPrice.Text = "Price";
        //
        // lblCode
        //
        lblCode.BackColor = Color.FromArgb(18, 19, 31);
        lblCode.Font = new Font("Consolas", 10F);
        lblCode.ForeColor = Color.FromArgb(232, 233, 245);
        lblCode.Location = new Point(430, 84);
        lblCode.Name = "lblCode";
        lblCode.Padding = new Padding(16);
        lblCode.Size = new Size(420, 300);
        lblCode.TabIndex = 2;
        //
        // FirstFunctionPage
        //
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoScroll = true;
        BackColor = Color.FromArgb(245, 246, 250);
        Controls.Add(lblCode);
        Controls.Add(panelCard);
        Controls.Add(lblExplain);
        Font = new Font("Segoe UI", 10F);
        Name = "FirstFunctionPage";
        Size = new Size(900, 600);
        panelCard.ResumeLayout(false);
        panelCard.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
        ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Label lblExplain;
    private Panel panelCard;
    private Label lblPrice;
    private NumericUpDown numPrice;
    private Label lblQuantity;
    private NumericUpDown numQuantity;
    private Button btnCalculate;
    private Label lblResult;
    private Label lblStock;
    private Label lblCode;
    private Label lblTryIt;
}
