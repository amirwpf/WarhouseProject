
using App.Domin.Core.Entities.TypedDataTables;

namespace Core.Entites
{
    public class StockItemQuantityReportDataSet : NonMasterDetailDataset<StockItemQuantityReportDataTable, StockItemQuantityReportRow>
    {
        public StockItemQuantityReportDataSet()
        {
            StockItemQuantityReportDataTable = new StockItemQuantityReportDataTable();
        }

        public StockItemQuantityReportDataTable StockItemQuantityReportDataTable { get; set; }

        public override StockItemQuantityReportDataTable MasterTable
        {
            get => StockItemQuantityReportDataTable;
            set => StockItemQuantityReportDataTable = value;
        }
    }
}
