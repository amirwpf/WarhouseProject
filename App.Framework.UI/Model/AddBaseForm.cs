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
    public partial class AddBaseForm : BaseForm
    {
        public AddBaseForm()
        {
            InitializeComponent();
        }

        private void AddBaseForm_Load(object sender, EventArgs e)
        {

        }

        private void InitializeToolTips()
        {

            ToolTip deleteBtnTT = new ToolTip();
            deleteBtnTT.SetToolTip(deleteBtn, "حذف");

            ToolTip saveBtnTT = new ToolTip();
            saveBtnTT.SetToolTip(saveBtn, "ذخیره");

            ToolTip addBtnTT = new ToolTip();
            addBtnTT.SetToolTip(addBtn, "جدید");
        }

        public virtual void deleteBtn_Click(object sender, EventArgs e)
        {

        }

        public virtual void addBtn_Click(object sender, EventArgs e)
        {

        }

        public virtual void SaveBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
