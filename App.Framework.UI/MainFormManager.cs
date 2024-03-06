using App.Framework.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace App.Framework.UI
{
    public static class MainFormManager
    {
        private static MainForm _mainForm;
        public static MainForm MainForm {
            get
            {
                if (_mainForm == null)
                    _mainForm = new MainForm();
                return _mainForm;
            }
            }

        public static void AddFormToMainForm(BaseForm baseForm)
        {
            baseForm.MdiParent = MainForm;

            baseForm.TabCtrl = MainForm.mainTabControl;

            TabPage tp = new TabPage();
            tp.Parent = MainForm.mainTabControl;
            tp.Text = baseForm.Text;
            tp.Show();

            baseForm.TabPag = tp;
            tp.Controls.Add(baseForm);

            baseForm.Show();

            MainForm.mainTabControl.SelectedTab = tp;
        }
    }
}
