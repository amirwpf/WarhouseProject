using Core.Entites;
using Infrastructure.Repositories;

namespace WarehouseTest.Services.TableIdService
{
    class TableIdServiceDAO : NonMasterDetailDAO<TableIdDataSet, TableIdTable, TableIdRow>
    {
        public TableIdServiceDAO() : base(new GenericRepository<TableIdTable, TableIdRow>())
        {

        }
    }
}
