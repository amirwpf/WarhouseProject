namespace WarehouseTest.forms
{
    partial class MainForm
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
            this.menuBox = new System.Windows.Forms.GroupBox();
            this.addPanel = new System.Windows.Forms.Panel();
            this.mainContaier = new System.Windows.Forms.SplitContainer();
            this.menuBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainContaier)).BeginInit();
            this.mainContaier.Panel1.SuspendLayout();
            this.mainContaier.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuBox
            // 
            this.menuBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuBox.Controls.Add(this.addPanel);
            this.menuBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menuBox.Location = new System.Drawing.Point(2, 6);
            this.menuBox.Name = "menuBox";
            this.menuBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuBox.Size = new System.Drawing.Size(125, 400);
            this.menuBox.TabIndex = 6;
            this.menuBox.TabStop = false;
            this.menuBox.Text = "عملیات";
            // 
            // addPanel
            // 
            this.addPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.addPanel.Location = new System.Drawing.Point(1, 19);
            this.addPanel.Name = "addPanel";
            this.addPanel.Size = new System.Drawing.Size(124, 177);
            this.addPanel.TabIndex = 7;
            // 
            // mainContaier
            // 
            this.mainContaier.Location = new System.Drawing.Point(2, 0);
            this.mainContaier.Name = "mainContaier";
            // 
            // mainContaier.Panel1
            // 
            this.mainContaier.Panel1.Controls.Add(this.menuBox);
            this.mainContaier.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.mainContaier.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // mainContaier.Panel2
            // 
            this.mainContaier.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.mainContaier.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.mainContaier.Size = new System.Drawing.Size(776, 429);
            this.mainContaier.SplitterDistance = 130;
            this.mainContaier.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 430);
            this.Controls.Add(this.mainContaier);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Text = "صفحه اصلی";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuBox.ResumeLayout(false);
            this.mainContaier.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainContaier)).EndInit();
            this.mainContaier.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox menuBox;
        private System.Windows.Forms.Panel addPanel;
        private System.Windows.Forms.SplitContainer mainContaier;
    }
}