using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseTest.UI.models;
using WarehouseTest.UI;
using Warehouse.Framework.Common;

namespace MainMenuExtentions
{
    class DeliveryListExt : IExtension
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
