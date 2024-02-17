
namespace Core.Entites
{
    public class StockDataSet : NonMasterDetailDataset<StockTable, StockRow>
    {
        public StockDataSet()
        {
            StockTable = new StockTable();
        }

        public StockTable StockTable { get; set; }

        public override StockTable MasterTable
        {
            get => StockTable;
            set => StockTable = value;
        }
    }
}