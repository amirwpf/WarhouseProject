using System;
using System.Data;
using System.Text;
using System.Linq;
using Core.Entites;
using Warehouse.Framework.Common;
using App.Domin.Core.Contracts.ServiceInterface;

namespace WarehouseTest.Services.ReceiptService
{
    public class ReceiptService : IReceiptService
    {
        private readonly ReceiptServiceDAO receiptServiceDAO;
        StringBuilder errorsMessageString;

        public ReceiptService()
        {
            receiptServiceDAO = new ReceiptServiceDAO();
            
        }

        public ReceiptDataset GetByMasterId(int ReceiptId)
        {
            var res = receiptServiceDAO.GetMasterDetailByMasterId(ReceiptId);
            return res;
        }

        public ReceiptDataset GetMasterAll()
        {
            return receiptServiceDAO.GetMasterAll();
        }

        public void Save(ReceiptDataset receiptDataset, object selectedItem, string receiptNumberText, DateTime receiptDate)
        {
            errorsMessageString = new StringBuilder();
            errorsMessageString.Append(ValidateStockSelection(receiptDataset , selectedItem));
            int receipNumber =ValidateReceiptNumber(receiptNumberText);
            ValidateData(receiptDataset);

            //receiptDataset.ReceiptTable.Add(receiptDataset.ReceiptTable.GetNewRow());
            receiptDataset.ReceiptTable[0].StockId = ((DataRowView)selectedItem).Row.Field<int>("Id");
            receiptDataset.ReceiptTable[0].Date = receiptDate;
            receiptDataset.ReceiptTable[0].Number = receipNumber;

            receiptServiceDAO.SaveMasterDetail(receiptDataset);


        }

        public void Save(ReceiptDataset receiptDataset)
        {
            errorsMessageString = new StringBuilder();
            ValidateData(receiptDataset);
            receiptServiceDAO.SaveMasterDetail(receiptDataset);
        }

        public void DeleteById(int ReceiptId)
        {
            receiptServiceDAO.DeleteMasterDetailByMasterId(ReceiptId);
        }

        private string ValidateStockSelection(ReceiptDataset receiptDataset, object selectedItem)
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

            receiptDataset.ReceiptTable[0].StockId = row.Field<int>("Id");
            return null;
        }


        private int ValidateReceiptNumber(string receiptNumberText)
        {
            if (!int.TryParse(receiptNumberText, out int receiptNumber))
            {
                errorsMessageString.Append(ErrorMessage.InValidFieldValue("شماره رسید انبار"));
            }
            return receiptNumber;
        }

        private void ValidateData(ReceiptDataset receiptDataset)
        {
            if (receiptDataset.ReceiptItemsTable.Rows.Count == 0)
            {
                errorsMessageString.Append(ErrorMessage.ItemCantBeEmpty("لیست کالا ها"));
            }
            else
            {
                foreach (var item in receiptDataset.ReceiptItemsTable.Where(x=>x.RowState!=DataRowState.Deleted))
                {
                    if (item.Quantity <= 0 || item.Quantity == null)
                        errorsMessageString.Append(ErrorMessage.ItemCantBeEmpty("تعداد کالا"));

                    if (item.ItemId == 0)
                        errorsMessageString.Append(ErrorMessage.ItemCantBeEmpty("کالا"));
                }
            }

            if (errorsMessageString.Length > 0)
            {
                throw new Exception(errorsMessageString.ToString());
            }
        }
    }
}

