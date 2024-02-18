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
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.menuBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainContaier)).BeginInit();
            this.mainContaier.Panel1.SuspendLayout();
            this.mainContaier.Panel2.SuspendLayout();
            this.mainContaier.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuBox
            // 
            this.menuBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuBox.Controls.Add(this.addPanel);
            this.menuBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.menuBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menuBox.Location = new System.Drawing.Point(0, 0);
            this.menuBox.Name = "menuBox";
            this.menuBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuBox.Size = new System.Drawing.Size(147, 474);
            this.menuBox.TabIndex = 6;
            this.menuBox.TabStop = false;
            this.menuBox.Text = "عملیات";
            // 
            // addPanel
            // 
            this.addPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.addPanel.Location = new System.Drawing.Point(1, 19);
            this.addPanel.Name = "addPanel";
            this.addPanel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.addPanel.Size = new System.Drawing.Size(146, 210);
            this.addPanel.TabIndex = 7;
            // 
            // mainContaier
            // 
            this.mainContaier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContaier.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.mainContaier.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mainContaier.IsSplitterFixed = true;
            this.mainContaier.Location = new System.Drawing.Point(0, 0);
            this.mainContaier.Name = "mainContaier";
            // 
            // mainContaier.Panel1
            // 
            this.mainContaier.Panel1.AllowDrop = true;
            this.mainContaier.Panel1.Controls.Add(this.menuBox);
            this.mainContaier.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // mainContaier.Panel2
            // 
            this.mainContaier.Panel2.Controls.Add(this.mainTabControl);
            this.mainContaier.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.mainContaier.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.mainContaier.Size = new System.Drawing.Size(880, 474);
            this.mainContaier.SplitterDistance = 147;
            this.mainContaier.TabIndex = 7;
            // 
            // mainTabControl
            // 
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Multiline = true;
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.RightToLeftLayout = true;
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(729, 474);
            this.mainTabControl.TabIndex = 0;
            this.mainTabControl.SelectedIndexChanged += new System.EventHandler(this.mainTabControl_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(880, 474);
            this.Controls.Add(this.mainContaier);
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "صفحه اصلی";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuBox.ResumeLayout(false);
            this.mainContaier.Panel1.ResumeLayout(false);
            this.mainContaier.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainContaier)).EndInit();
            this.mainContaier.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox menuBox;
        private System.Windows.Forms.Panel addPanel;
        private System.Windows.Forms.SplitContainer mainContaier;
        private System.Windows.Forms.TabControl mainTabControl;
    }
}