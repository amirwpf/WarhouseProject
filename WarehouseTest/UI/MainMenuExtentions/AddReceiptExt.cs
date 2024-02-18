using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Framework.Common;
using WarehouseTest.UI.models;
using WarehouseTest;

namespace MainMenuExtentions
{
    public class AddReceiptExt : IExtension
    {
        public string Name => "سند ورود جدید";

        public int Order => 5;

        public BaseForm Btn_Click()
        {
            AddReceiptForm addReceiptForm = new AddReceiptForm();
            return addReceiptForm;
        }
    }
}
