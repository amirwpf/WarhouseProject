
using System.Data;
using App.Framework;

namespace Core.Entites
{
    public class StockDataSet : BaseDataSet
    {
        public StockDataSet()
        {
            StockTable = new StockTable();
        }

        public StockTable StockTable { get; set; }

        public override BaseDataTable MasterTable => StockTable;
    }
}