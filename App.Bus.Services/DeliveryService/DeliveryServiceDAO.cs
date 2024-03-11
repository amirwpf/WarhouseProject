using App.Framework;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace WarehouseTest.Services.DeliveryService
{
    public class DeliveryServiceDAO : MasterDetailDAO<DeliveryDataset>
    {
        public DeliveryServiceDAO() : base()
        {

        }

        public DeliveryDataset GetByStockId(int StockId)
        {
            DeliveryDataset deliveryDataset = new DeliveryDataset();
            var tableName = deliveryDataset.DeliveryTable.TableName;

            string query = $"SELECT * FROM {tableName} WHERE StockId = @Id";
            SqlParameter[] parameters = { new SqlParameter("@Id", StockId) };

            _repository.ExecuteQuery(query, parameters, deliveryDataset.DeliveryTable);

            return deliveryDataset;
        }

        public DeliveryDataset GetByItemId(int itemId)
        {
            DeliveryDataset deliveryDataset = new DeliveryDataset();
            var tableName = deliveryDataset.DeliveryItemsTable.TableName;

            string query = $"SELECT * FROM {tableName} WHERE ItemId = @Id";
            SqlParameter[] parameters = { new SqlParameter("@Id", itemId) };

            _repository.ExecuteQuery(query, parameters, deliveryDataset.DeliveryItemsTable);

            return deliveryDataset;
        }
    }
}
