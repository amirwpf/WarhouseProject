using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Framework.Common;
using WarehouseTest.UI;
using WarehouseTest.UI.models;

namespace MainMenuExtentions
{
    public class AddDeliveryExt : IExtension
    {
        public string Name => "سند خروج جدید";

        public int Order => 7;

        public BaseForm Btn_Click()
        {
            AddDeliveryForm addDeliveryForm = new AddDeliveryForm();
            return addDeliveryForm;
        }
    }
}
