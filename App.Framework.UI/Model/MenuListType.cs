using App.Framework.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.UI
{
    public class MenuListType
    {
        public Func<BaseListForm> Form { get; set; }
        public string FormTitle { get; set; }
        public int Order { get; set; }

        public string CategoryName { get; set; }
    }
}
