using App.Domin.Core;
using App.Domin.Core.Contracts.ServiceInterface;
using Core.Entites;

namespace WarehouseTest.Services.TableIdService
{
    public class TableIdService : ITableIdService
    {
        private readonly TableIdServiceDAO tableIdServiceDAO;
        public TableIdService()
        {
            tableIdServiceDAO = new TableIdServiceDAO();
        }

        public int GetId(DbTablesEnum DbTablesEnum)
        {
            TableIdDataSet tableIdDataSet = new TableIdDataSet();
            int idNumber = 0;
            var id = (int)DbTablesEnum;

            tableIdDataSet = tableIdServiceDAO.GetById(id);
            if(tableIdDataSet != null)
            {
                idNumber = tableIdDataSet.TableIdTable[0].IdNumber;
                tableIdDataSet.TableIdTable[0].IdNumber += 1;
            }
            tableIdServiceDAO.Save(tableIdDataSet);

            return idNumber;
        }

    }
}
