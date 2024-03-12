using System.Data.SqlClient;

namespace App.Framework
{
    class TableIdServiceDAO : BaseDAO<TableIdDataSet>
    {
        public TableIdServiceDAO() : base()
        {

        }
        public TableIdDataSet GetByTableName(string tableName)
        {
            TableIdDataSet tableIdDataSet = new TableIdDataSet();
            var databaseTableName = tableIdDataSet.TableIdTable.TableName;

            string query = $"SELECT * FROM {databaseTableName} WHERE [TableName] = @Name";
            SqlParameter[] parameters = { new SqlParameter("@Name", tableName) };

            _repository.ExecuteQuery(query, parameters, tableIdDataSet.TableIdTable);

            return tableIdDataSet;
        }

    }
}
