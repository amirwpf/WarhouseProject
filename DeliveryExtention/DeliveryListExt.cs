using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Framework.Common;
using WarehouseTest.UI;
using WarehouseTest.UI.models;

namespace DeliveryExtention
{
    class DeliveryListExt: IExtension
    {
        public string Name => "سند خروج";

        public int Order => 4;

        public BaseForm Btn_Click()
        {
            DeliveryListForm deliveryListForm = new DeliveryListForm();
            return deliveryListForm;
        }
    }
}
