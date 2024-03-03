using App.Domin.Core.Contracts.ServiceInterface;
using App.Framework;
using App.Framework.UI;
using App.Framework.UI.Model;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WarehouseTest.forms;

namespace WarehouseTest.UI.models
{
    [ExtentionMenuAttribute(CategoryName = "Warehouse")]
    public class ListInitializer : IMenuListInitializer
    {

        public List<MenuListType> GetMenuLists()
        {
            var menuLists = new List<MenuListType>()
            {
                new MenuListType()
                {
                    Form = typeof(BaseListFormGeneric<DeliveryDataset, DeliveryTable, DeliveryRow, AddDeliveryForm,IDeliveryService>),
                    FormTitle = "خروج",
                    Order=4
                },
                new MenuListType()
                {
                    Form = typeof(BaseListFormGeneric<ReceiptDataset, ReceiptTable, ReceiptRow, AddReceiptForm,IReceiptService>),
                    FormTitle = "ورود",
                    Order=3
                },
                new MenuListType()
                {
                    Form = typeof(BaseListFormGeneric<ItemDataSet, ItemTable, ItemRow, AddItemForm,IItemService>),
                    FormTitle = "کالا",
                    Order=1
                },
                new MenuListType()
                {
                    Form = typeof(BaseListFormGeneric<StockDataSet, StockTable, StockRow, AddStockForm,IStockService>),
                    FormTitle = "انبار",
                    Order=2
                },
            };


            return menuLists;
        }
    }
}
