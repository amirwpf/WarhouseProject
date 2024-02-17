using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Framework.Common;
using WarehouseTest;
using WarehouseTest.UI;
using WarehouseTest.UI.models;

namespace ReceiptExtention
{
    public class AddReceiptExt : IExtension
    {
        public string Name => "سند ورود جدید";

        public int Order => 1;

        public BaseForm Btn_Click()
        {
            AddReceiptForm addReceiptForm = new AddReceiptForm();
            return addReceiptForm;
        }
    }
}
