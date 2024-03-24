using App.Domin.Core;
using App.Domin.Core.Contracts.ServiceInterface;
using App.Framework;
using App.Framework.Entities.DataRows;
using Core.Entites;
using System;
using System.Data;
using System.Linq;
using System.Text;
using WarehouseTest.Services.DeliveryService;
using WarehouseTest.Services.ReceiptService;

namespace WarehouseTest.Services.StockService
{
    public class StockService : IStockService
    {
        private readonly StockServiceDAO _stockServiceDAO;
        private readonly IReceiptService _receiptService;
        private readonly IDeliveryService _deliveryService;

        public StockService()
        {
            _stockServiceDAO = new StockServiceDAO();
            ServiceFactory serviceFactory = new ServiceFactory();
            _receiptService = serviceFactory.Resolve<IReceiptService>();
            _deliveryService = serviceFactory.Resolve<IDeliveryService>();
        }

        public StockDataSet GetById(int itemId)
        {
            return _stockServiceDAO.GetById(itemId);
        }

        public StockDataSet GetAll()
        {
            return _stockServiceDAO.GetAll();
        }

        public void Save(StockDataSet stockDataSet)
        {
            if (stockDataSet.StockTable.Rows.Count > 0)
            {
                StockRow row = stockDataSet.StockTable[0];
                if (row.RowState != DataRowState.Deleted)
                {
                    var codeInt = ValidateData(row.Id, row.Name, row.Code.ToString());
                }
            }
            _stockServiceDAO.Save(stockDataSet);
        }

        public void DeleteById(int stockId)
        {
            CheckForStockUsage(stockId);
            _stockServiceDAO.Delete(stockId);
        }

        public void DeleteWithcheckVersion(StockDataSet stockDataSet, IdDataRow row)
        {
            var originalIdVersion = 0;
            if (row != null)
                originalIdVersion = (int)(row.GetOriginalPropertyValue(nameof(row.ID)));

            CheckForStockUsage(originalIdVersion);
            _stockServiceDAO.DeleteWithcheckVersion(stockDataSet, row);
        }


        private void CheckForStockUsage(int stockId)
        {
            var itemRecList = _receiptService.GetByStockId(stockId).ReceiptTable;
            var itemDelList = _deliveryService.GetByStockId(stockId).DeliveryTable;
            var errorMsg = new StringBuilder();
            var stock = _stockServiceDAO.GetById(stockId).StockTable.FirstOrDefault();

            if (stock != null)
            {
                foreach (var item in itemRecList)
                {
                    errorMsg.Append($"انبار {stock.Name} در ورود کد {item.Number} استفاده شده است \n ");
                }

                foreach (var item in itemDelList)
                {
                    errorMsg.Append($"انبار {stock.Name} در خروج کد {item.Number} استفاده شده است \n ");
                }

                if (itemRecList.Count() > 0 || itemDelList.Count() > 0)
                {
                    throw new Exception(errorMsg.ToString());
                }
            }
        }

        public int ValidateData(int id, string name, string code)
        {
            var errorsMessageString = new StringBuilder();
            ValidateName(name, id, errorsMessageString);
            var codeInt = ValidateCode(id, code, errorsMessageString);

            if (errorsMessageString.Length > 0)
            {
                throw new Exception(errorsMessageString.ToString());
            }
            return codeInt;
        }

        public void ValidateName(string name, int id, StringBuilder errorsMessageString)
        {
            if (string.IsNullOrEmpty(name))
            {
                errorsMessageString.Append(ErrorMessage.ItemCantBeEmpty("نام"));
            }

            var stockTable = _stockServiceDAO.GetAll().StockTable;

            foreach (var stock in stockTable)
            {
                if (stock.Name == name && id != 0)
                {
                    var stockRowId = stock.Id;
                    if (stockRowId != id)
                    {
                        errorsMessageString.Append(ErrorMessage.RepititiveValue("نام"));
                        break;
                    }
                }
                if (stock.Name == name && id == 0)
                {
                    errorsMessageString.Append(ErrorMessage.RepititiveValue("نام"));
                    break;
                }
            }
        }

        public int ValidateCode(int id, string code, StringBuilder errorsMessageString)
        {
            int codeInt = 0;
            if (string.IsNullOrEmpty(code))
            {
                errorsMessageString.Append(ErrorMessage.ItemCantBeEmpty("کد"));
            }
            bool validCode = int.TryParse(code, out codeInt);
            if (!validCode || codeInt <= 0)
            {
                errorsMessageString.Append(ErrorMessage.InValidFieldValue("کد"));
            }
            var stockTable = _stockServiceDAO.GetAll().StockTable;

            foreach (var stock in stockTable)
            {
                if (stock.Code == codeInt && id != 0)
                {
                    var stockRowId = stock.Id;
                    if (stockRowId != id)
                    {
                        errorsMessageString.Append(ErrorMessage.RepititiveValue("کد"));
                        break;
                    }
                }
                if (stock.Code == codeInt && id == 0)
                {
                    errorsMessageString.Append(ErrorMessage.RepititiveValue("کد"));
                    break;
                }
            }
            return codeInt;
        }

        public DataTable GetStockItemQuantityReport()
        {
            var res = _stockServiceDAO.GetStockItemQuantityReport();
            return res;
        }
    }
}
