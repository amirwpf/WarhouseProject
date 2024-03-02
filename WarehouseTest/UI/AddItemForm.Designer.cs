namespace WarehouseTest.forms
{
    partial class AddItemForm
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
            this.itemNameTx = new System.Windows.Forms.TextBox();
            this.itemNameLbl = new System.Windows.Forms.Label();
            this.itemCodeLbl = new System.Windows.Forms.Label();
            this.itemCodeTxt = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // itemNameTx
            // 
            this.itemNameTx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemNameTx.Location = new System.Drawing.Point(55, 123);
            this.itemNameTx.Name = "itemNameTx";
            this.itemNameTx.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.itemNameTx.Size = new System.Drawing.Size(529, 20);
            this.itemNameTx.TabIndex = 0;
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
            // itemCodeLbl
            // 
            this.itemCodeLbl.AutoSize = true;
            this.itemCodeLbl.Location = new System.Drawing.Point(29, 88);
            this.itemCodeLbl.Name = "itemCodeLbl";
            this.itemCodeLbl.Size = new System.Drawing.Size(19, 13);
            this.itemCodeLbl.TabIndex = 3;
            this.itemCodeLbl.Text = "کد";
            // 
            // itemCodeTxt
            // 
            this.itemCodeTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemCodeTxt.Location = new System.Drawing.Point(55, 85);
            this.itemCodeTxt.Name = "itemCodeTxt";
            this.itemCodeTxt.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.itemCodeTxt.Size = new System.Drawing.Size(529, 20);
            this.itemCodeTxt.TabIndex = 2;
            // 
            // AddItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 387);
            this.Controls.Add(this.itemCodeTxt);
            this.Controls.Add(this.itemNameTx);
            this.Controls.Add(this.itemCodeLbl);
            this.Controls.Add(this.itemNameLbl);
            this.Name = "AddItemForm";
            this.Text = "کالا جدید";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AddItemForm_Load);
            this.Controls.SetChildIndex(this.itemNameLbl, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.itemCodeLbl, 0);
            this.Controls.SetChildIndex(this.itemNameTx, 0);
            this.Controls.SetChildIndex(this.itemCodeTxt, 0);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox itemNameTx;
        private System.Windows.Forms.Label itemNameLbl;
        private System.Windows.Forms.Label itemCodeLbl;
        private System.Windows.Forms.TextBox itemCodeTxt;
    }
}