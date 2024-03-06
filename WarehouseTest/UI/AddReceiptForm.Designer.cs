namespace WarehouseTest
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
            this.receiptDatePicker = new System.Windows.Forms.DateTimePicker();
            this.stockCombo = new System.Windows.Forms.ComboBox();
            this.deleteItemBtn = new System.Windows.Forms.Button();
            this.receiptNumberTxt = new NumericTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // addBtn
            // 
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // addItemBtn
            // 
            this.addItemBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addItemBtn.AutoSize = true;
            this.addItemBtn.BackColor = System.Drawing.Color.Moccasin;
            this.addItemBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addItemBtn.Location = new System.Drawing.Point(11, 354);
            this.addItemBtn.Name = "addItemBtn";
            this.addItemBtn.Size = new System.Drawing.Size(31, 27);
            this.addItemBtn.TabIndex = 31;
            this.addItemBtn.Text = "+";
            this.addItemBtn.UseVisualStyleBackColor = false;
            this.addItemBtn.Click += new System.EventHandler(this.addItemBtn_Click);
            // 
            // itemDataGrid
            // 
            this.itemDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.itemDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.itemDataGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.itemDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.itemDataGrid.Location = new System.Drawing.Point(12, 169);
            this.itemDataGrid.Name = "itemDataGrid";
            this.itemDataGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.itemDataGrid.Size = new System.Drawing.Size(595, 184);
            this.itemDataGrid.TabIndex = 30;
            this.itemDataGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.itemDataGrid_CellEndEdit);
            this.itemDataGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.itemDataGrid_DataError);
            // 
            // receiptDateLbl
            // 
            this.receiptDateLbl.AutoSize = true;
            this.receiptDateLbl.Location = new System.Drawing.Point(12, 131);
            this.receiptDateLbl.Name = "receiptDateLbl";
            this.receiptDateLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.receiptDateLbl.Size = new System.Drawing.Size(78, 13);
            this.receiptDateLbl.TabIndex = 29;
            this.receiptDateLbl.Text = "تاریخ سند ورود";
            // 
            // receiptNumberLbl
            // 
            this.receiptNumberLbl.AutoSize = true;
            this.receiptNumberLbl.Location = new System.Drawing.Point(12, 99);
            this.receiptNumberLbl.Name = "receiptNumberLbl";
            this.receiptNumberLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.receiptNumberLbl.Size = new System.Drawing.Size(82, 13);
            this.receiptNumberLbl.TabIndex = 28;
            this.receiptNumberLbl.Text = "شماره سند ورود";
            // 
            // stockLbl
            // 
            this.stockLbl.AutoSize = true;
            this.stockLbl.Location = new System.Drawing.Point(12, 69);
            this.stockLbl.Name = "stockLbl";
            this.stockLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.stockLbl.Size = new System.Drawing.Size(27, 13);
            this.stockLbl.TabIndex = 27;
            this.stockLbl.Text = "انبار";
            // 
            // receiptDatePicker
            // 
            this.receiptDatePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.receiptDatePicker.Location = new System.Drawing.Point(100, 125);
            this.receiptDatePicker.Name = "receiptDatePicker";
            this.receiptDatePicker.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.receiptDatePicker.RightToLeftLayout = true;
            this.receiptDatePicker.Size = new System.Drawing.Size(505, 20);
            this.receiptDatePicker.TabIndex = 25;
            // 
            // stockCombo
            // 
            this.stockCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stockCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stockCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stockCombo.FormattingEnabled = true;
            this.stockCombo.Location = new System.Drawing.Point(100, 66);
            this.stockCombo.Name = "stockCombo";
            this.stockCombo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.stockCombo.Size = new System.Drawing.Size(505, 21);
            this.stockCombo.TabIndex = 24;
            this.stockCombo.SelectedIndexChanged += new System.EventHandler(this.stockCombo_SelectedIndexChanged);
            // 
            // deleteItemBtn
            // 
            this.deleteItemBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteItemBtn.AutoSize = true;
            this.deleteItemBtn.BackColor = System.Drawing.Color.Red;
            this.deleteItemBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteItemBtn.Location = new System.Drawing.Point(42, 354);
            this.deleteItemBtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.deleteItemBtn.Name = "deleteItemBtn";
            this.deleteItemBtn.Size = new System.Drawing.Size(31, 27);
            this.deleteItemBtn.TabIndex = 32;
            this.deleteItemBtn.Text = "x";
            this.deleteItemBtn.UseVisualStyleBackColor = false;
            this.deleteItemBtn.Click += new System.EventHandler(this.deleteItemBtn_Click);
            // 
            // receiptNumberTxt
            // 
            this.receiptNumberTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.receiptNumberTxt.Location = new System.Drawing.Point(100, 96);
            this.receiptNumberTxt.Name = "receiptNumberTxt";
            this.receiptNumberTxt.Size = new System.Drawing.Size(505, 20);
            this.receiptNumberTxt.TabIndex = 33;
            // 
            // AddReceiptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 387);
            this.Controls.Add(this.receiptNumberTxt);
            this.Controls.Add(this.deleteItemBtn);
            this.Controls.Add(this.addItemBtn);
            this.Controls.Add(this.itemDataGrid);
            this.Controls.Add(this.receiptDateLbl);
            this.Controls.Add(this.receiptNumberLbl);
            this.Controls.Add(this.stockLbl);
            this.Controls.Add(this.receiptDatePicker);
            this.Controls.Add(this.stockCombo);
            this.Location = new System.Drawing.Point(600, 300);
            this.Name = "AddReceiptForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ورود انبار";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AddReceiptForm_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.stockCombo, 0);
            this.Controls.SetChildIndex(this.receiptDatePicker, 0);
            this.Controls.SetChildIndex(this.stockLbl, 0);
            this.Controls.SetChildIndex(this.receiptNumberLbl, 0);
            this.Controls.SetChildIndex(this.receiptDateLbl, 0);
            this.Controls.SetChildIndex(this.itemDataGrid, 0);
            this.Controls.SetChildIndex(this.addItemBtn, 0);
            this.Controls.SetChildIndex(this.deleteItemBtn, 0);
            this.Controls.SetChildIndex(this.receiptNumberTxt, 0);
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
        private System.Windows.Forms.DateTimePicker receiptDatePicker;
        private System.Windows.Forms.ComboBox stockCombo;
        public System.Windows.Forms.Button deleteItemBtn;
        private NumericTextBox receiptNumberTxt;
    }
}

