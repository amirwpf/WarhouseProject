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
                    Form = GetNewDeliveryFormFunc,
                    FormTitle = "خروج",
                    Order=4
                },
                new MenuListType()
                {
                    Form = GetNewReceiptFormFunc,
                    FormTitle = "ورود",
                    Order=3
                },
                new MenuListType()
                {
                    Form = GetNewItemFormFunc,
                    FormTitle = "کالا",
                    Order=1
                },
                new MenuListType()
                {
                    Form = GetNewStockFormFunc,
                    FormTitle = "انبار",
                    Order=2
                },
                new MenuListType()
                {
                    Form = GetNewReportFormFunc,
                    FormTitle = "گزارش تعداد کالای انبار",
                    Order=10
                },
            };


            return menuLists;
        }

        public BaseListForm GetNewReportFormFunc()
        {
            var form = new StockItemQuantityReportForm();
            return form;
        }

        public BaseListForm GetNewStockFormFunc()
        {
            var form = new BaseListFormGeneric<StockDataSet, StockRow, IStockService>();
            form.NewForm += () => new AddStockForm();
            form.EditForm += id => new AddStockForm(id);
            return form;
        }

        public BaseListForm GetNewItemFormFunc()
        {
            var form = new BaseListFormGeneric<ItemDataSet, ItemRow, IItemService>();
            form.NewForm += () => new AddItemForm();
            form.EditForm += id => new AddItemForm(id);
            return form;
        }

        public BaseListForm GetNewReceiptFormFunc()
        {
            var form = new BaseListFormGeneric<ReceiptDataset, ReceiptRow, IReceiptService>();
            form.NewForm += () => new AddReceiptForm();
            form.EditForm += id => new AddReceiptForm(id);
            return form;
        }


        public BaseListForm GetNewDeliveryFormFunc()
        {
            var form = new BaseListFormGeneric<DeliveryDataset, DeliveryRow, IDeliveryService>();
            form.NewForm += () => new AddDeliveryForm();
            form.EditForm += id => new AddDeliveryForm(id);
            return form;
        }

    }
}
