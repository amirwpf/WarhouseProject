
namespace App.Framework
{
    public class TableIdService : ITableIdService
    {
        private readonly TableIdServiceDAO tableIdServiceDAO;
        public TableIdService()
        {
            tableIdServiceDAO = new TableIdServiceDAO();
        }

        public int GetId(string tableName)
        {
            TableIdDataSet tableIdDataSet = new TableIdDataSet();
            int idNumber = 0;
            //var id = (int)DbTablesEnum;

            tableIdDataSet = tableIdServiceDAO.GetByTableName(tableName);
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
