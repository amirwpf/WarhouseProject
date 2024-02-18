﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WarehouseTest.UI.models
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {

        }

        internal virtual void SaveBtn_Click(object sender, EventArgs e)
        {

        }

        internal virtual void deleteBtn_Click(object sender, EventArgs e)
        {

        }

        internal virtual void addBtn_Click(object sender, EventArgs e)
        {

        }

        internal virtual void refreshBtn_Click(object sender, EventArgs e)
        {

        }

        private TabControl tabCtrl = new TabControl();
        private TabPage tabPag = new TabPage();

        public TabPage TabPag
        {
            get
            {
                return tabPag;
            }
            set
            {
                tabPag = value;
            }
        }

        public TabControl TabCtrl
        {
            set
            {
                tabCtrl = value;
            }
        }

        private void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.tabPag.Dispose();

            //if (!tabCtrl.HasChildren)
            //{
            //    tabCtrl.Visible = false;
            //}
        }

        private void BaseForm_Activated(object sender, EventArgs e)
        {
            tabCtrl.SelectedTab = tabPag;

            //if (!tabCtrl.Visible)
            //{
            //    tabCtrl.Visible = true;
            //}
        }
    }
}
