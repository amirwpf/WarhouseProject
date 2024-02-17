using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Framework.Common;
using WarehouseTest.UI.models;
using WarehouseTest.UI;

namespace MainMenuExtentions
{
    class StockListExt : IExtension
    {
        public string Name => "انبار";

        public int Order => 8;

        public BaseForm Btn_Click()
        {
            StockListForm stockListForm = new StockListForm();
            return stockListForm;
        }
    }
}
