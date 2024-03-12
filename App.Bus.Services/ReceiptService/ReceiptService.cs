using System;
using System.Data;
using System.Text;
using System.Linq;
using Core.Entites;
using App.Domin.Core.Contracts.ServiceInterface;
using App.Domin.Core;
using App.Framework;

namespace WarehouseTest.Services.ReceiptService
{
    public class ReceiptService : IReceiptService
    {
        private readonly ReceiptServiceDAO receiptServiceDAO;

        public ReceiptService()
        {
            receiptServiceDAO = new ReceiptServiceDAO();
            
        }

        public ReceiptDataset GetById(int id)
        {
            var res = receiptServiceDAO.GetMasterDetailById(id);
            return res;
        }

        public ReceiptDataset GetByStockId(int stockId)
        {
            var res = receiptServiceDAO.GetByStockId(stockId);
            return res;
        }

        public ReceiptDataset GetByItemId(int itemId)
        {
            var res = receiptServiceDAO.GetByItemId(itemId);
            return res;
        }

        public ReceiptDataset GetAll()
        {
            return receiptServiceDAO.GetAll();
        }

        public void Save(ReceiptDataset receiptDataset)
        {
            ValidateData(receiptDataset);

            receiptServiceDAO.Save(receiptDataset);
        }

        public void DeleteById(int ReceiptId)
        {
            receiptServiceDAO.Delete(ReceiptId);
        }

        private bool ValidateReceiptNumber(int id ,int receiptNumber)
        {
            var receiptTable = receiptServiceDAO.GetAll().ReceiptTable;
            foreach (var receipt in receiptTable)
            {
                if (receipt.Number == receiptNumber && receipt.Id!=id)
                {
                    return false;
                }
            }
            return true;
        }

        private void ValidateData(ReceiptDataset receiptDataset)
        {
            var errorsMessageString = new StringBuilder();

            if(!ValidateReceiptNumber(receiptDataset.ReceiptTable[0].Id, receiptDataset.ReceiptTable[0].Number))
            {
                errorsMessageString.Append(ErrorMessage.RepititiveValue("شماره سند ورود"));
            }

            if (receiptDataset.ReceiptItemsTable.Rows.Count == 0)
            {
                errorsMessageString.Append(ErrorMessage.ItemCantBeEmpty("لیست کالا ها"));
            }
            else
            {
                foreach (var item in receiptDataset.ReceiptItemsTable.Where(x=>x.RowState!=DataRowState.Deleted))
                {
                    if (item.Quantity <= 0 || item.Quantity == null)
                        errorsMessageString.Append(ErrorMessage.ValueMustBePositive("تعداد کالا"));

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