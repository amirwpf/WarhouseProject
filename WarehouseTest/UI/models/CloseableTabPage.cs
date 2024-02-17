using System;
using System.Drawing;
using System.Windows.Forms;

namespace WarehouseTest.forms
{
    public class CloseableTabPage : TabPage
    {
        public Button closeButton;

        public CloseableTabPage()
        {
            closeButton = new Button()
            {
                Size = new System.Drawing.Size(20, 20),
                Text = "x",
                TextAlign = ContentAlignment.MiddleCenter
            };

            closeButton.Click += CloseButton_Click;
            //Controls.Add(closeButton);
        }

        //public void SetCloseButtonLocation(int x, int y)
        //{
        //    closeButton.Location = new System.Drawing.Point(x, y);
        //}

        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Would you like to close this tab?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var parentTabControl = Parent as TabControl;
                parentTabControl?.TabPages.Remove(this);
            }
        }
    }
}