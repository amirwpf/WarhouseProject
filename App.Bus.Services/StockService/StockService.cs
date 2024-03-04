using App.Domin.Core;
using App.Domin.Core.Contracts.ServiceInterface;
using App.Framework;
using Core.Entites;
using System;
using System.Data;
using System.Text;

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

        }

        public StockDataSet GetById(int itemId)
        {
            return stockServiceDAO.GetById(itemId);
        }

        public StockDataSet GetAll()
        {
            return stockServiceDAO.GetAll();
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
                stockDataSet = stockServiceDAO.GetById(id);
                stockDataSet.StockTable[0].Name = name;
                stockDataSet.StockTable[0].Code = codeInt;
            }

            stockServiceDAO.Save(stockDataSet);
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

        public void DeleteById(int itemId)
        {
            stockServiceDAO.Delete(itemId);
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

            var stockTable = stockServiceDAO.GetAll().StockTable;

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
            var stockTable = stockServiceDAO.GetAll().StockTable;

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
            var res = stockServiceDAO.GetStockItemQuantityReport();
            return res;
        }
    }
}
