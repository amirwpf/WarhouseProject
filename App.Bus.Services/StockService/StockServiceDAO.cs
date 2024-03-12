using App.Framework;
using Core.Entites;
using System.Data;
using System.Data.SqlClient;

namespace WarehouseTest.Services.StockService
{
    class StockServiceDAO : BaseDAO<StockDataSet>
    {
        public StockServiceDAO() : base()
        {

        }

        public DataTable GetStockItemQuantityReport()
        {
            DataTable dataTable = new DataTable();
            SqlParameter[] parameters = { };
            string query = "select z.Id,z.Name,SUM(z.sum) as Quantity From ( select s.Id,s.Name,-di.Quantity as sum from deliveryItems di inner join delivery d ON d.Id=di.DeliveryId inner join stock s ON d.stockId=s.id union select s.Id,s.Name,ri.Quantity as sum from receiptItem ri inner join receipt r ON r.Id=ri.ReceiptId inner join stock s ON r.stockId=s.id ) as z group by z.Id,z.Name";
            _repository.ExecuteQuery(query, parameters, dataTable);
            return dataTable;
        }
    }
}
