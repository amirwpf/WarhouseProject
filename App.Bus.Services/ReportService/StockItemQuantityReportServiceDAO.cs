using App.Domin.Core.Entities.TypedDataTables;
using Core.Entites;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Bus.Services.ReportService
{
    class StockItemQuantityReportServiceDAO : NonMasterDetailDAO<StockItemQuantityReportDataSet, StockItemQuantityReportDataTable, StockItemQuantityReportRow>
    {
        public StockItemQuantityReportServiceDAO() : base(new GenericRepository<StockItemQuantityReportDataTable, StockItemQuantityReportRow>())
        {

        }
    }
}
