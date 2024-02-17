﻿namespace WarehouseTest
{
    partial class AddReceiptForm
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
            this.addItemBtn = new System.Windows.Forms.Button();
            this.itemDataGrid = new System.Windows.Forms.DataGridView();
            this.receiptDateLbl = new System.Windows.Forms.Label();
            this.receiptNumberLbl = new System.Windows.Forms.Label();
            this.stockLbl = new System.Windows.Forms.Label();
            this.receiptNumberTxt = new System.Windows.Forms.TextBox();
            this.receiptDatePicker = new System.Windows.Forms.DateTimePicker();
            this.stockCombo = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // addItemBtn
            // 
            this.addItemBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.addItemBtn.AutoSize = true;
            this.addItemBtn.BackColor = System.Drawing.Color.Moccasin;
            this.addItemBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addItemBtn.Location = new System.Drawing.Point(12, 297);
            this.addItemBtn.Name = "addItemBtn";
            this.addItemBtn.Size = new System.Drawing.Size(26, 27);
            this.addItemBtn.TabIndex = 31;
            this.addItemBtn.Text = "+";
            this.addItemBtn.UseVisualStyleBackColor = false;
            this.addItemBtn.Click += new System.EventHandler(this.addItemBtn_Click);
            // 
            // itemDataGrid
            // 
            this.itemDataGrid.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.itemDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.itemDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.itemDataGrid.Location = new System.Drawing.Point(12, 174);
            this.itemDataGrid.Name = "itemDataGrid";
            this.itemDataGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.itemDataGrid.Size = new System.Drawing.Size(594, 150);
            this.itemDataGrid.TabIndex = 30;
            // 
            // receiptDateLbl
            // 
            this.receiptDateLbl.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.receiptDateLbl.AutoSize = true;
            this.receiptDateLbl.Location = new System.Drawing.Point(521, 128);
            this.receiptDateLbl.Name = "receiptDateLbl";
            this.receiptDateLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.receiptDateLbl.Size = new System.Drawing.Size(78, 13);
            this.receiptDateLbl.TabIndex = 29;
            this.receiptDateLbl.Text = "تاریخ سند ورود";
            // 
            // receiptNumberLbl
            // 
            this.receiptNumberLbl.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.receiptNumberLbl.AutoSize = true;
            this.receiptNumberLbl.Location = new System.Drawing.Point(517, 96);
            this.receiptNumberLbl.Name = "receiptNumberLbl";
            this.receiptNumberLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.receiptNumberLbl.Size = new System.Drawing.Size(82, 13);
            this.receiptNumberLbl.TabIndex = 28;
            this.receiptNumberLbl.Text = "شماره سند ورود";
            // 
            // stockLbl
            // 
            this.stockLbl.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.stockLbl.AutoSize = true;
            this.stockLbl.Location = new System.Drawing.Point(576, 66);
            this.stockLbl.Name = "stockLbl";
            this.stockLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.stockLbl.Size = new System.Drawing.Size(27, 13);
            this.stockLbl.TabIndex = 27;
            this.stockLbl.Text = "انبار";
            // 
            // receiptNumberTxt
            // 
            this.receiptNumberTxt.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.receiptNumberTxt.Location = new System.Drawing.Point(12, 93);
            this.receiptNumberTxt.Name = "receiptNumberTxt";
            this.receiptNumberTxt.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.receiptNumberTxt.Size = new System.Drawing.Size(495, 20);
            this.receiptNumberTxt.TabIndex = 26;
            // 
            // receiptDatePicker
            // 
            this.receiptDatePicker.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.receiptDatePicker.Location = new System.Drawing.Point(12, 122);
            this.receiptDatePicker.Name = "receiptDatePicker";
            this.receiptDatePicker.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.receiptDatePicker.RightToLeftLayout = true;
            this.receiptDatePicker.Size = new System.Drawing.Size(495, 20);
            this.receiptDatePicker.TabIndex = 25;
            // 
            // stockCombo
            // 
            this.stockCombo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.stockCombo.FormattingEnabled = true;
            this.stockCombo.Location = new System.Drawing.Point(12, 63);
            this.stockCombo.Name = "stockCombo";
            this.stockCombo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.stockCombo.Size = new System.Drawing.Size(495, 21);
            this.stockCombo.TabIndex = 24;
            this.stockCombo.SelectedIndexChanged += new System.EventHandler(this.stockCombo_SelectedIndexChanged);
            // 
            // AddReceiptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 387);
            this.Controls.Add(this.addItemBtn);
            this.Controls.Add(this.itemDataGrid);
            this.Controls.Add(this.receiptDateLbl);
            this.Controls.Add(this.receiptNumberLbl);
            this.Controls.Add(this.stockLbl);
            this.Controls.Add(this.receiptNumberTxt);
            this.Controls.Add(this.receiptDatePicker);
            this.Controls.Add(this.stockCombo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddReceiptForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "سند ورود جدید";
            this.Load += new System.EventHandler(this.AddReceiptForm_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.stockCombo, 0);
            this.Controls.SetChildIndex(this.receiptDatePicker, 0);
            this.Controls.SetChildIndex(this.receiptNumberTxt, 0);
            this.Controls.SetChildIndex(this.stockLbl, 0);
            this.Controls.SetChildIndex(this.receiptNumberLbl, 0);
            this.Controls.SetChildIndex(this.receiptDateLbl, 0);
            this.Controls.SetChildIndex(this.itemDataGrid, 0);
            this.Controls.SetChildIndex(this.addItemBtn, 0);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.itemDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button addItemBtn;
        public System.Windows.Forms.DataGridView itemDataGrid;
        private System.Windows.Forms.Label receiptDateLbl;
        private System.Windows.Forms.Label receiptNumberLbl;
        private System.Windows.Forms.Label stockLbl;
        private System.Windows.Forms.TextBox receiptNumberTxt;
        private System.Windows.Forms.DateTimePicker receiptDatePicker;
        private System.Windows.Forms.ComboBox stockCombo;
    }
}

