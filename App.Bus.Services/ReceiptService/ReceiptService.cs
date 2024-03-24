using System;
using System.Data;
using System.Text;
using System.Linq;
using Core.Entites;
using App.Domin.Core.Contracts.ServiceInterface;
using App.Domin.Core;
using App.Framework;
using App.Framework.Entities.DataRows;

namespace WarehouseTest.Services.ReceiptService
{
    public class ReceiptService : IReceiptService
    {
        private readonly ReceiptServiceDAO _receiptServiceDAO;

        public ReceiptService()
        {
            _receiptServiceDAO = new ReceiptServiceDAO();
            
        }

        public ReceiptDataset GetById(int id)
        {
            var res = _receiptServiceDAO.GetMasterDetailById(id);
            return res;
        }

        public ReceiptDataset GetByStockId(int stockId)
        {
            var res = _receiptServiceDAO.GetByStockId(stockId);
            return res;
        }

        public ReceiptDataset GetByItemId(int itemId)
        {
            var res = _receiptServiceDAO.GetByItemId(itemId);
            return res;
        }

        public ReceiptDataset GetAll()
        {
            return _receiptServiceDAO.GetAll();
        }

        public void Save(ReceiptDataset receiptDataset)
        {
            if(receiptDataset.ReceiptTable[0].RowState!=DataRowState.Deleted)
                ValidateData(receiptDataset);

            _receiptServiceDAO.Save(receiptDataset);
        }

        public void DeleteById(int ReceiptId)
        {
            _receiptServiceDAO.Delete(ReceiptId);
        }

        private bool ValidateReceiptNumber(int id ,int receiptNumber)
        {
            var receiptTable = _receiptServiceDAO.GetAll().ReceiptTable;
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

        public void DeleteWithcheckVersion(ReceiptDataset dataSet, IdDataRow row)
        {
            _receiptServiceDAO.DeleteWithcheckVersion(dataSet, row);
        }
    }
}