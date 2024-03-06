namespace WarehouseTest.UI
{
    partial class AddStockForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.stockNameTx = new System.Windows.Forms.TextBox();
            this.itemCodeLbl = new System.Windows.Forms.Label();
            this.itemNameLbl = new System.Windows.Forms.Label();
            this.stockCodeTxt = new NumericTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addBtn
            // 
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // stockNameTx
            // 
            this.stockNameTx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stockNameTx.Location = new System.Drawing.Point(55, 123);
            this.stockNameTx.Name = "stockNameTx";
            this.stockNameTx.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.stockNameTx.Size = new System.Drawing.Size(529, 20);
            this.stockNameTx.TabIndex = 8;
            // 
            // itemCodeLbl
            // 
            this.itemCodeLbl.AutoSize = true;
            this.itemCodeLbl.Location = new System.Drawing.Point(29, 88);
            this.itemCodeLbl.Name = "itemCodeLbl";
            this.itemCodeLbl.Size = new System.Drawing.Size(19, 13);
            this.itemCodeLbl.TabIndex = 3;
            this.itemCodeLbl.Text = "کد";
            this.itemCodeLbl.Click += new System.EventHandler(this.itemCodeLbl_Click);
            // 
            // itemNameLbl
            // 
            this.itemNameLbl.AutoSize = true;
            this.itemNameLbl.Location = new System.Drawing.Point(29, 126);
            this.itemNameLbl.Name = "itemNameLbl";
            this.itemNameLbl.Size = new System.Drawing.Size(20, 13);
            this.itemNameLbl.TabIndex = 1;
            this.itemNameLbl.Text = "نام";
            // 
            // stockCodeTxt
            // 
            this.stockCodeTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stockCodeTxt.Location = new System.Drawing.Point(55, 85);
            this.stockCodeTxt.Name = "stockCodeTxt";
            this.stockCodeTxt.Size = new System.Drawing.Size(529, 20);
            this.stockCodeTxt.TabIndex = 0;
            // 
            // AddStockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 387);
            this.Controls.Add(this.stockCodeTxt);
            this.Controls.Add(this.stockNameTx);
            this.Controls.Add(this.itemCodeLbl);
            this.Controls.Add(this.itemNameLbl);
            this.Name = "AddStockForm";
            this.ShowIcon = false;
            this.Text = "انبار";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AddStockForm_Load);
            this.Controls.SetChildIndex(this.itemNameLbl, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.itemCodeLbl, 0);
            this.Controls.SetChildIndex(this.stockNameTx, 0);
            this.Controls.SetChildIndex(this.stockCodeTxt, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox stockNameTx;
        private System.Windows.Forms.Label itemCodeLbl;
        private System.Windows.Forms.Label itemNameLbl;
        private NumericTextBox stockCodeTxt;
    }
}