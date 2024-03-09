namespace WarehouseTest.UI
{
    partial class AddDeliveryForm
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
            this.deliveryDateLbl = new System.Windows.Forms.Label();
            this.deliveryNumberLbl = new System.Windows.Forms.Label();
            this.stockLbl = new System.Windows.Forms.Label();
            this.stockCombo = new System.Windows.Forms.ComboBox();
            this.deleteItemBtn = new System.Windows.Forms.Button();
            this.deliveryNumberTxt = new NumericTextBox();
            this.persianDate = new PersianDateTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            // 
            // saveBtn
            // 
            this.saveBtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            // 
            // addBtn
            // 
            this.addBtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // deleteBtn
            // 
            this.deleteBtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            // 
            // addItemBtn
            // 
            this.addItemBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addItemBtn.AutoSize = true;
            this.addItemBtn.BackColor = System.Drawing.Color.Moccasin;
            this.addItemBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addItemBtn.Location = new System.Drawing.Point(11, 354);
            this.addItemBtn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.addItemBtn.Name = "addItemBtn";
            this.addItemBtn.Size = new System.Drawing.Size(31, 27);
            this.addItemBtn.TabIndex = 23;
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
            this.itemDataGrid.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.itemDataGrid.Name = "itemDataGrid";
            this.itemDataGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.itemDataGrid.Size = new System.Drawing.Size(595, 184);
            this.itemDataGrid.TabIndex = 22;
            this.itemDataGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.itemDataGrid_DataError);
            // 
            // deliveryDateLbl
            // 
            this.deliveryDateLbl.AutoSize = true;
            this.deliveryDateLbl.Location = new System.Drawing.Point(12, 131);
            this.deliveryDateLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.deliveryDateLbl.Name = "deliveryDateLbl";
            this.deliveryDateLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.deliveryDateLbl.Size = new System.Drawing.Size(82, 13);
            this.deliveryDateLbl.TabIndex = 21;
            this.deliveryDateLbl.Text = "تاریخ سند خروج";
            // 
            // deliveryNumberLbl
            // 
            this.deliveryNumberLbl.AutoSize = true;
            this.deliveryNumberLbl.Location = new System.Drawing.Point(12, 99);
            this.deliveryNumberLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.deliveryNumberLbl.Name = "deliveryNumberLbl";
            this.deliveryNumberLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.deliveryNumberLbl.Size = new System.Drawing.Size(86, 13);
            this.deliveryNumberLbl.TabIndex = 20;
            this.deliveryNumberLbl.Text = "شماره سند خروج";
            // 
            // stockLbl
            // 
            this.stockLbl.AutoSize = true;
            this.stockLbl.Location = new System.Drawing.Point(12, 69);
            this.stockLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.stockLbl.Name = "stockLbl";
            this.stockLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.stockLbl.Size = new System.Drawing.Size(27, 13);
            this.stockLbl.TabIndex = 19;
            this.stockLbl.Text = "انبار";
            // 
            // stockCombo
            // 
            this.stockCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stockCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stockCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stockCombo.FormattingEnabled = true;
            this.stockCombo.Location = new System.Drawing.Point(100, 66);
            this.stockCombo.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.stockCombo.Name = "stockCombo";
            this.stockCombo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.stockCombo.Size = new System.Drawing.Size(505, 21);
            this.stockCombo.TabIndex = 16;
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
            this.deleteItemBtn.TabIndex = 24;
            this.deleteItemBtn.Text = "x";
            this.deleteItemBtn.UseVisualStyleBackColor = false;
            this.deleteItemBtn.Click += new System.EventHandler(this.deleteItemBtn_Click);
            // 
            // deliveryNumberTxt
            // 
            this.deliveryNumberTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deliveryNumberTxt.Location = new System.Drawing.Point(100, 96);
            this.deliveryNumberTxt.Name = "deliveryNumberTxt";
            this.deliveryNumberTxt.Size = new System.Drawing.Size(505, 20);
            this.deliveryNumberTxt.TabIndex = 25;
            // 
            // persianDate
            // 
            this.persianDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.persianDate.Date = new System.DateTime(2024, 3, 9, 10, 50, 43, 68);
            this.persianDate.Location = new System.Drawing.Point(99, 128);
            this.persianDate.Name = "persianDate";
            this.persianDate.Size = new System.Drawing.Size(505, 20);
            this.persianDate.TabIndex = 26;
            this.persianDate.Text = "____/__/__";
            // 
            // AddDeliveryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 387);
            this.Controls.Add(this.persianDate);
            this.Controls.Add(this.deliveryNumberTxt);
            this.Controls.Add(this.deleteItemBtn);
            this.Controls.Add(this.addItemBtn);
            this.Controls.Add(this.itemDataGrid);
            this.Controls.Add(this.deliveryDateLbl);
            this.Controls.Add(this.deliveryNumberLbl);
            this.Controls.Add(this.stockLbl);
            this.Controls.Add(this.stockCombo);
            this.Location = new System.Drawing.Point(600, 300);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "AddDeliveryForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "خروج انبار";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AddDeliveryForm_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.stockCombo, 0);
            this.Controls.SetChildIndex(this.stockLbl, 0);
            this.Controls.SetChildIndex(this.deliveryNumberLbl, 0);
            this.Controls.SetChildIndex(this.deliveryDateLbl, 0);
            this.Controls.SetChildIndex(this.itemDataGrid, 0);
            this.Controls.SetChildIndex(this.addItemBtn, 0);
            this.Controls.SetChildIndex(this.deleteItemBtn, 0);
            this.Controls.SetChildIndex(this.deliveryNumberTxt, 0);
            this.Controls.SetChildIndex(this.persianDate, 0);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.itemDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button addItemBtn;
        public System.Windows.Forms.DataGridView itemDataGrid;
        private System.Windows.Forms.Label deliveryDateLbl;
        private System.Windows.Forms.Label deliveryNumberLbl;
        private System.Windows.Forms.Label stockLbl;
        private System.Windows.Forms.ComboBox stockCombo;
        public System.Windows.Forms.Button deleteItemBtn;
        private NumericTextBox deliveryNumberTxt;
        private PersianDateTextBox persianDate;
    }
}