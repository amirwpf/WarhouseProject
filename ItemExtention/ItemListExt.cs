using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Framework.Common;
using WarehouseTest.UI;
using WarehouseTest.UI.models;

namespace ItemExtention
{
    class ItemListExt : IExtension
    {
        public string Name => "کالا";

        public int Order => 6;

        public BaseForm Btn_Click()
        {
            ItemList itemList = new ItemList();
            return itemList;
        }
    }
}
