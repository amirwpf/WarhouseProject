using App.Framework;
using Core.Entites;
using System.Data.SqlClient;

namespace WarehouseTest.Services.ReceiptService
{
    public class ReceiptServiceDAO : MasterDetailDAO<ReceiptDataset, ReceiptTable, ReceiptItemsTable, ReceiptRow, ReceiptItemsRow>
    {
        public ReceiptServiceDAO() : base(new GenericRepository<ReceiptTable, ReceiptRow>(), new GenericRepository<ReceiptItemsTable, ReceiptItemsRow>())
        {

        }

        public ReceiptDataset GetByStockId(int StockId)
        {
            ReceiptDataset receiptdataSet = new ReceiptDataset();
            var tableName = receiptdataSet.ReceiptTable.TableName;

            string query = $"SELECT * FROM {tableName} WHERE StockId = @Id";
            SqlParameter[] parameters = { new SqlParameter("@Id", StockId) };

            masterRepository.ExecuteQuery(query, parameters, receiptdataSet.ReceiptTable);

            return receiptdataSet;
        }

        public ReceiptDataset GetByItemId(int itemId)
        {
            ReceiptDataset receiptdataSet = new ReceiptDataset();
            var tableName = receiptdataSet.ReceiptItemsTable.TableName;

            string query = $"SELECT * FROM {tableName} WHERE ItemId = @Id";
            SqlParameter[] parameters = { new SqlParameter("@Id", itemId) };

            detailRepository.ExecuteQuery(query, parameters, receiptdataSet.ReceiptItemsTable);

            return receiptdataSet;
        }
    }
}
