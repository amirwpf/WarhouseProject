using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Framework.Common;
using WarehouseTest.forms;
using WarehouseTest.UI.models;

namespace MainMenuExtentions
{
    public class AddItemExt : IExtension
    {
        public string Name => "کالا جدید";

        public int Order => 1;

        public BaseForm Btn_Click()
        {
            AddItemForm addItemForm = new AddItemForm();
            return addItemForm;
        }
    }
}
