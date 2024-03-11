using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace App.Framework.UI
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

        }

        private void BaseForm_Activated(object sender, EventArgs e)
        {
            tabCtrl.SelectedTab = tabPag;

        }
    }
}
