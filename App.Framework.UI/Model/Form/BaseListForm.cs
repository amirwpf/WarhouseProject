using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace App.Framework.UI.Model
{
    public partial class BaseListForm :BaseForm
    {

        public BaseListForm()
        {
            InitializeComponent();
            InitializeToolTips();
        }

        private void InitializeToolTips()
        {

            ToolTip deleteBtnTT = new ToolTip();
            deleteBtnTT.SetToolTip(deleteBtn, "حذف");

            ToolTip saveBtnTT = new ToolTip();
            saveBtnTT.SetToolTip(refreshBtn, "نوسازی");

            ToolTip addBtnTT = new ToolTip();
            addBtnTT.SetToolTip(addBtn, "جدید");
        }

        public virtual void deleteBtn_Click(object sender, EventArgs e)
        {

        }

        private void itemDataGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        protected virtual void refreshBtn_Click(object sender, EventArgs e)
        {

        }

        protected virtual void addBtn_Click(object sender, EventArgs e)
        {

        }

        protected virtual void dataGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

    }
}
