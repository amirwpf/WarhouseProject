using Core.Entites;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarehouseTest.Services.ItemService
{
    class ItemServiceDAO : NonMasterDetailDAO<ItemDataSet,ItemTable,ItemRow>
    {
        public ItemServiceDAO():base(new GenericRepository<ItemTable, ItemRow>())
        {

        }
    }
}
