
using System;
using System.Linq;

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

            tableIdDataSet = tableIdServiceDAO.GetByTableName(tableName);
            if (tableIdDataSet.TableIdTable.Count() > 0)
            {
                idNumber = tableIdDataSet.TableIdTable[0].IdNumber;
                tableIdDataSet.TableIdTable[0].IdNumber += 1;
            }
            else
            {
                throw new Exception();
            }
            tableIdServiceDAO.Save(tableIdDataSet);

            return idNumber;
        }

    }
}
