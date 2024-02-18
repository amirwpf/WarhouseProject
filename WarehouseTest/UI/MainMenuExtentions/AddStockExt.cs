using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Framework.Common;
using WarehouseTest.UI.models;
using WarehouseTest.UI;

namespace MainMenuExtentions
{
    public class AddStockExt : IExtension
    {
        public string Name => "انبار جدید";

        public int Order => 3;

        public BaseForm Btn_Click()
        {
            AddStockForm addStockForm = new AddStockForm();
            return addStockForm;
        }
    }
}
