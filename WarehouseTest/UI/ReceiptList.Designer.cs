namespace WarehouseTest.UI
{
    partial class ReceiptList
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
            this.receiptDataGrid = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.receiptDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // receiptDataGrid
            // 
            this.receiptDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.receiptDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.receiptDataGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.receiptDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.receiptDataGrid.Location = new System.Drawing.Point(12, 57);
            this.receiptDataGrid.Name = "receiptDataGrid";
            this.receiptDataGrid.ReadOnly = true;
            this.receiptDataGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.receiptDataGrid.RowHeadersWidth = 72;
            this.receiptDataGrid.Size = new System.Drawing.Size(594, 318);
            this.receiptDataGrid.TabIndex = 0;
            this.receiptDataGrid.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.receiptDataGrid_CellMouseDoubleClick);
            // 
            // ReceiptList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(618, 387);
            this.Controls.Add(this.receiptDataGrid);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ReceiptList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "سند ورود";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ReceiptList_Load);
            this.Controls.SetChildIndex(this.receiptDataGrid, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.receiptDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView receiptDataGrid;
    }
}