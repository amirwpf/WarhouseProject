using System.Data.SqlClient;

namespace App.Framework
{
    class TableIdServiceDAO : NonMasterDetailDAO<TableIdDataSet, TableIdTable, TableIdRow>
    {
        public TableIdServiceDAO() : base(new GenericRepository<TableIdTable, TableIdRow>())
        {

        }
        public TableIdDataSet GetByTableName(string tableName)
        {
            TableIdDataSet tableIdDataSet = new TableIdDataSet();
            var databaseTableName = tableIdDataSet.TableIdTable.TableName;

            string query = $"SELECT * FROM {databaseTableName} WHERE [TableName] = @Name";
            SqlParameter[] parameters = { new SqlParameter("@Name", tableName) };

            repository.ExecuteQuery(query, parameters, tableIdDataSet.TableIdTable);

            return tableIdDataSet;
        }

    }
}
