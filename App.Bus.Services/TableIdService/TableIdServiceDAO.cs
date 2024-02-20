using App.Framework;
using Core.Entites;

namespace WarehouseTest.Services.TableIdService
{
    class TableIdServiceDAO : NonMasterDetailDAO<TableIdDataSet, TableIdTable, TableIdRow>
    {
        public TableIdServiceDAO() : base(new GenericRepository<TableIdTable, TableIdRow>())
        {

        }
    }
}
