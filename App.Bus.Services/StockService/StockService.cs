using App.Domin.Core.Contracts.ServiceInterface;
using Core.Entites;
using System;
using System.Text;
using Warehouse.Framework.Common;

namespace WarehouseTest.Services.StockService
{
    public class StockService : IStockService
    {
        private readonly StockServiceDAO stockServiceDAO;
        TableIdService.TableIdService tableIdService;
        StringBuilder errorsMessageString;
        StockDataSet stockDataSet;
        StockRow newStockRow;
        bool updateRow;
        public StockService()
        {
            stockServiceDAO = new StockServiceDAO();
            tableIdService = new TableIdService.TableIdService();
            stockDataSet = new StockDataSet();
            newStockRow = stockDataSet.StockTable.GetNewRow();
            newStockRow.Id = tableIdService.GetId(DbTablesEnum.stock);
            updateRow = true;
        }

        public StockDataSet GetById(int itemId)
        {
            return stockServiceDAO.GetById(itemId);
        }

        public StockDataSet GetAll()
        {
            return stockServiceDAO.GetAll();
        }

        public void Save(string name, string code)
        {
            var codeInt = ValidateData(name, code);

            newStockRow.Name = name;
            newStockRow.Code = codeInt;
            if (updateRow)
            {
                updateRow = false;
                stockDataSet.StockTable.Add(newStockRow);
            }

            stockServiceDAO.Save(stockDataSet);
        }

        public void Save(StockDataSet stockDataSet)
        {
            ValidateDataSet(stockDataSet);
            stockServiceDAO.Save(stockDataSet);
        }

        public void ValidateDataSet(StockDataSet stockDataSet)
        {
            foreach (var stock in stockDataSet.StockTable)
            {
                ValidateData(stock.Name, stock.Code.ToString());
            }
        }

        public void DeleteById(int itemId)
        {
            stockServiceDAO.Delete(itemId);
        }


        public int ValidateData(string name, string code)
        {
            errorsMessageString = new StringBuilder();
            ValidateName(name);
            var codeInt = ValidateCode(code);

            if (errorsMessageString.Length > 0)
            {
                throw new Exception(errorsMessageString.ToString());
            }
            return codeInt;
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                errorsMessageString.Append(ErrorMessage.ItemCantBeEmpty("نام"));
            }
        }

        public int ValidateCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                errorsMessageString.Append(ErrorMessage.ItemCantBeEmpty("کد"));
            }
            bool validCode = int.TryParse(code, out int codeInt);
            if (!validCode || codeInt <= 0)
            {
                errorsMessageString.Append(ErrorMessage.InValidFieldValue("کد"));
            }
            else
            {
                return codeInt;
            }
            return 0;
        }
    }
}
