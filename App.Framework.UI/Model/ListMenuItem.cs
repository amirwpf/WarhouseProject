using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace App.Framework.UI.Model
{
    class ListMenuItem : Label
    {
        public Func<BaseListForm> GetListFormFunc { get; set; }

        public ListMenuItem(string title)
        {
            Text = title;
            Dock = DockStyle.Top;
            TextAlign = ContentAlignment.MiddleLeft;
            Cursor = Cursors.Hand;
            RightToLeft = RightToLeft.Yes;
        }
    }
}
