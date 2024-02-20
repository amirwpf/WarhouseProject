namespace App.Framework.UI
{
    public partial class MainForm
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
            this.warehouseMenuBtn = new System.Windows.Forms.Button();
            this.addPanel = new System.Windows.Forms.Panel();
            this.mainContaier = new System.Windows.Forms.SplitContainer();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
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
            this.menuBox.Controls.Add(this.button3);
            this.menuBox.Controls.Add(this.button2);
            this.menuBox.Controls.Add(this.button1);
            this.menuBox.Controls.Add(this.warehouseMenuBtn);
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
            // warehouseMenuBtn
            // 
            this.warehouseMenuBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.warehouseMenuBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.warehouseMenuBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.warehouseMenuBtn.Location = new System.Drawing.Point(1, 275);
            this.warehouseMenuBtn.Name = "warehouseMenuBtn";
            this.warehouseMenuBtn.Size = new System.Drawing.Size(147, 39);
            this.warehouseMenuBtn.TabIndex = 8;
            this.warehouseMenuBtn.Text = "انبار";
            this.warehouseMenuBtn.UseVisualStyleBackColor = false;
            this.warehouseMenuBtn.Click += new System.EventHandler(this.warehouseMenuBtn_Click);
            this.warehouseMenuBtn.MouseEnter += new System.EventHandler(this.warehouseMenuBtn_MouseEnter);
            this.warehouseMenuBtn.MouseLeave += new System.EventHandler(this.warehouseMenuBtn_MouseLeave);
            // 
            // addPanel
            // 
            this.addPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.addPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.addPanel.Location = new System.Drawing.Point(1, 19);
            this.addPanel.Name = "addPanel";
            this.addPanel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.addPanel.Size = new System.Drawing.Size(146, 250);
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
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(1, 320);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 39);
            this.button1.TabIndex = 9;
            this.button1.Text = "حقوق و دستمزد";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(1, 365);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(147, 39);
            this.button2.TabIndex = 10;
            this.button2.Text = "خرده فروشی";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(0, 410);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(147, 39);
            this.button3.TabIndex = 11;
            this.button3.Text = "سفارش کار";
            this.button3.UseVisualStyleBackColor = false;
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
        private System.Windows.Forms.Button warehouseMenuBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
    }
}