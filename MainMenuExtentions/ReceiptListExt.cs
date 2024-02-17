using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Framework.Common;
using WarehouseTest.UI.models;
using WarehouseTest.UI;

namespace MainMenuExtentions
{
    public class ReceiptListExt : IExtension
    {
        public string Name => "سند ورود";

        public int Order => 2;

        public BaseForm Btn_Click()
        {
            ReceiptList receiptList = new ReceiptList();
            return receiptList;
        }
    }
}
