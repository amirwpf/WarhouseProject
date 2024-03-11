using App.Framework;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarehouseTest.Services.ItemService
{
    class ItemServiceDAO : NonMasterDetailDAO<ItemDataSet>
    {
        public ItemServiceDAO():base()
        {

        }
    }
}
