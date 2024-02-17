namespace WarehouseTest.UI
{
    partial class ItemDetailForm
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.itemNameTx = new System.Windows.Forms.TextBox();
            this.itemCodeLbl = new System.Windows.Forms.Label();
            this.itemNameLbl = new System.Windows.Forms.Label();
            this.itemCodeTxt = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.itemNameTx);
            this.panel2.Controls.Add(this.itemCodeLbl);
            this.panel2.Controls.Add(this.itemNameLbl);
            this.panel2.Controls.Add(this.itemCodeTxt);
            this.panel2.Location = new System.Drawing.Point(15, 78);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(591, 129);
            this.panel2.TabIndex = 5;
            // 
            // itemNameTx
            // 
            this.itemNameTx.Location = new System.Drawing.Point(52, 16);
            this.itemNameTx.Name = "itemNameTx";
            this.itemNameTx.Size = new System.Drawing.Size(533, 20);
            this.itemNameTx.TabIndex = 0;
            // 
            // itemCodeLbl
            // 
            this.itemCodeLbl.AutoSize = true;
            this.itemCodeLbl.Location = new System.Drawing.Point(11, 78);
            this.itemCodeLbl.Name = "itemCodeLbl";
            this.itemCodeLbl.Size = new System.Drawing.Size(32, 13);
            this.itemCodeLbl.TabIndex = 3;
            this.itemCodeLbl.Text = "Code";
            // 
            // itemNameLbl
            // 
            this.itemNameLbl.AutoSize = true;
            this.itemNameLbl.Location = new System.Drawing.Point(11, 19);
            this.itemNameLbl.Name = "itemNameLbl";
            this.itemNameLbl.Size = new System.Drawing.Size(35, 13);
            this.itemNameLbl.TabIndex = 1;
            this.itemNameLbl.Text = "Name";
            // 
            // itemCodeTxt
            // 
            this.itemCodeTxt.Location = new System.Drawing.Point(52, 75);
            this.itemCodeTxt.Name = "itemCodeTxt";
            this.itemCodeTxt.Size = new System.Drawing.Size(533, 20);
            this.itemCodeTxt.TabIndex = 2;
            // 
            // ItemDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 387);
            this.Controls.Add(this.panel2);
            this.Name = "ItemDetailForm";
            this.Text = "ItemDetailForm";
            this.Load += new System.EventHandler(this.ItemDetailForm_Load);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox itemNameTx;
        private System.Windows.Forms.Label itemCodeLbl;
        private System.Windows.Forms.Label itemNameLbl;
        private System.Windows.Forms.TextBox itemCodeTxt;
    }
}