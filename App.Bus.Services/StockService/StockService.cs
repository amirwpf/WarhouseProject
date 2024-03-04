using App.Domin.Core;
using App.Domin.Core.Contracts.ServiceInterface;
using App.Framework;
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
        TableIdService.TableIdService tableIdService;
        StringBuilder errorsMessageString;
        StockDataSet stockDataSet;
        StockRow newStockRow;
        bool updateRow;
        public StockService()
        {
            _stockServiceDAO = new StockServiceDAO();
            tableIdService = new TableIdService.TableIdService();
            stockDataSet = new StockDataSet();
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

        public void Save(int id, string name, string code)
        {
            var codeInt = ValidateData(id, name, code);


            if (id == 0)
            {
                newStockRow = stockDataSet.StockTable.GetNewRow();
                newStockRow.Id = tableIdService.GetId(DbTablesEnum.stock);
                newStockRow.Name = name;
                newStockRow.Code = codeInt;
                updateRow = false;
                stockDataSet.StockTable.Add(newStockRow);
            }
            else
            {
                stockDataSet = _stockServiceDAO.GetById(id);
                stockDataSet.StockTable[0].Name = name;
                stockDataSet.StockTable[0].Code = codeInt;
            }

            _stockServiceDAO.Save(stockDataSet);
        }

        //public void Save(StockDataSet stockDataSet)
        //{
        //    ValidateDataSet(stockDataSet);
        //    stockServiceDAO.Save(stockDataSet);
        //}

        //public void ValidateDataSet(StockDataSet stockDataSet)
        //{
        //    foreach (var stock in stockDataSet.StockTable)
        //    {
        //        ValidateData(stock.Name, stock.Code.ToString());
        //    }
        //}

        public void DeleteById(int stockId)
        {
            var itemRecList = _receiptService.GetAll().ReceiptTable.Where(x => x.StockId == stockId);
            var itemDelList = _deliveryService.GetAll().DeliveryTable.Where(x => x.StockId == stockId);
            var errorMsg = new StringBuilder();
            var stock = _stockServiceDAO.GetById(stockId).StockTable[0];

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
            _stockServiceDAO.Delete(stockId);
        }


        public int ValidateData(int id, string name, string code)
        {
            errorsMessageString = new StringBuilder();
            ValidateName(name,id);
            var codeInt = ValidateCode(id, code);

            if (errorsMessageString.Length > 0)
            {
                throw new Exception(errorsMessageString.ToString());
            }
            return codeInt;
        }

        public void ValidateName(string name,int id)
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

        public int ValidateCode(int id, string code)
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
