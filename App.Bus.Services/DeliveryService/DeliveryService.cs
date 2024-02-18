using App.Domin.Core.Contracts.ServiceInterface;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Warehouse.Framework.Common;
using WarehouseTest.Services.StockService;

namespace WarehouseTest.Services.DeliveryService
{
    public class DeliveryService : IDeliveryService
    {
        private readonly DeliveryServiceDAO deliveryServiceDAO;
        StringBuilder errorsMessageString;
        public DeliveryService()
        {
            deliveryServiceDAO = new DeliveryServiceDAO();
        }

        public DeliveryDataset GetByMasterId(int deliveryId)
        {
            return deliveryServiceDAO.GetMasterDetailByMasterId(deliveryId);
        }

        public DeliveryDataset GetMasterAll()
        {
            return deliveryServiceDAO.GetMasterAll();
        }

        public void Save(DeliveryDataset deliveryDataset, object selectedItem, string deliveryNumberText, DateTime deliveryDate)
        {
            errorsMessageString = new StringBuilder();
            //var receipNumber =ValidateData(deliveryDataset, selectedItem, deliveryNumberText);
            errorsMessageString.Append(ValidateStockSelection(deliveryDataset, selectedItem));
            int receipNumber = ValidateReceiptNumber(deliveryNumberText);
            ValidateData(deliveryDataset);

            deliveryDataset.DeliveryTable[0].StockId = ((DataRowView)selectedItem).Row.Field<int>("Id");
            deliveryDataset.DeliveryTable[0].Date = deliveryDate;
            deliveryDataset.DeliveryTable[0].Number = receipNumber;

            deliveryServiceDAO.SaveMasterDetail(deliveryDataset);


        }

        //public void Save(DeliveryDataset deliveryDataset)
        //{
        //    errorsMessageString = new StringBuilder();
        //    ValidateData(deliveryDataset);
        //    deliveryServiceDAO.SaveMasterDetail(deliveryDataset);
        //}

        public void DeleteById(int deliveryId)
        {
            deliveryServiceDAO.DeleteMasterDetailByMasterId(deliveryId);
        }

        private void ValidateData(DeliveryDataset deliveryDataset)
        {
            if (deliveryDataset.DeliveryItemsTable.Rows.Count == 0)
            {
                errorsMessageString.Append(ErrorMessage.ItemCantBeEmpty("لیست کالا"));
            }
            else
            {
                foreach (var item in deliveryDataset.DeliveryItemsTable.Where(x => x.RowState != DataRowState.Deleted))
                {
                    if (item.Quantity <= 0 || item.Quantity == null)
                        errorsMessageString.Append(ErrorMessage.ItemCantBeEmpty(" تعداد کالا"));

                    if (item.ItemId == 0)
                        errorsMessageString.Append(ErrorMessage.ItemCantBeEmpty("کالا"));
                }
            }

            if (errorsMessageString.Length > 0)
            {
                throw new Exception(errorsMessageString.ToString());
            }
        }


        //private int ValidateData(DeliveryDataset deliveryDataset, object selectedItem, string deliveryNumberText)
        //{

        //    errorsMessageString.Append(ValidateStockSelection(deliveryDataset, selectedItem));
        //    int receipNumber = ValidateReceiptNumber(deliveryNumberText);



        //    if (deliveryDataset.DeliveryItemsTable.Rows.Count == 0)
        //    {
        //        errorsMessageString.Append(ErrorMessage.ItemCantBeEmpty("Receipt Items"));
        //    }
        //    else
        //    {
        //        foreach (var item in deliveryDataset.DeliveryItemsTable.Where(x => x.RowState != DataRowState.Deleted))
        //        {
        //            if (item.Quantity <= 0 || item.Quantity == null)
        //                errorsMessageString.Append(ErrorMessage.ItemCantBeEmpty("Item Quantity"));

        //            if (item.ItemId == 0)
        //                errorsMessageString.Append(ErrorMessage.ItemCantBeEmpty("Item"));
        //        }
        //    }

        //    if (errorsMessageString.Length > 0)
        //    {
        //        throw new Exception(errorsMessageString.ToString());
        //    }

        //    return receipNumber;
        //}


        private string ValidateStockSelection(DeliveryDataset deliveryDataset, object selectedItem)
        {
            if (selectedItem == null)
            {
                return ErrorMessage.InValidFieldValue("انبار");
            }

            if (!(selectedItem is DataRowView rowView))
            {
                return ErrorMessage.InValidFieldValue("انبار");
            }

            DataRow row = rowView.Row;

            if (row == null)
            {
                return ErrorMessage.InValidFieldValue("انبار");
            }

            if (!row.Table.Columns.Contains("Id"))
            {
                return ErrorMessage.InValidFieldValue("انبار");
            }

            deliveryDataset.DeliveryTable[0].StockId = row.Field<int>("Id");
            return null;
        }


        private int ValidateReceiptNumber(string receiptNumberText)
        {
            if (!int.TryParse(receiptNumberText, out int receiptNumber))
            {
                errorsMessageString.Append(ErrorMessage.InValidFieldValue("شماره خروج انبار"));
            }
            return receiptNumber;
        }
    }
}
