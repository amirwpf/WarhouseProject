using App.Domin.Core.Contracts.ServiceInterface;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Bus.Services.ReportService
{
    public class ReportService : IReportService
    {
        private readonly StockItemQuantityReportServiceDAO stockItemQuantityReportServiceDAO;
        public ReportService()
        {
            stockItemQuantityReportServiceDAO = new StockItemQuantityReportServiceDAO();
        }

        public StockItemQuantityReportDataSet GetStockItemQuantityReport()
        {
            var res = stockItemQuantityReportServiceDAO.GetAll();
            return res;
        }
    }

    //public class TableIdService : ITableIdService
    //{
    //    private readonly TableIdServiceDAO tableIdServiceDAO;
    //    public TableIdService()
    //    {
    //        tableIdServiceDAO = new TableIdServiceDAO();
    //    }

    //    public int GetId(DbTablesEnum DbTablesEnum)
    //    {
    //        TableIdDataSet tableIdDataSet = new TableIdDataSet();
    //        int idNumber = 0;
    //        var id = (int)DbTablesEnum;

    //        tableIdDataSet = tableIdServiceDAO.GetById(id);
    //        if (tableIdDataSet != null)
    //        {
    //            idNumber = tableIdDataSet.TableIdTable[0].IdNumber;
    //            tableIdDataSet.TableIdTable[0].IdNumber += 1;
    //        }
    //        tableIdServiceDAO.Save(tableIdDataSet);

    //        return idNumber;
    //    }
    //}
}
