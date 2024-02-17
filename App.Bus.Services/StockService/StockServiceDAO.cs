using Core.Entites;
using Infrastructure.Repositories;

namespace WarehouseTest.Services.StockService
{
    class StockServiceDAO : NonMasterDetailDAO<StockDataSet, StockTable, StockRow>
    {
        public StockServiceDAO() : base(new GenericRepository<StockTable, StockRow>())
        {

        }
    }
}
