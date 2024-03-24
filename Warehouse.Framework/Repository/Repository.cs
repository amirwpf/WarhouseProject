using App.Framework.Entities.DataRows;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace App.Framework
{
    public class Repository:IDisposable
    {
        private static Repository _repository;

        public static Repository GetRepository()
        {
            if (_repository == null) _repository = new Repository();

            return _repository;
        }

        private readonly SqlConnection _connection;

        private Repository()
        {
            _connection = new SqlConnection(StaticFields.connectionString);
            _connection.Open();
        }

        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
        public void FetchAll(BaseDataTable dataTable)
        {
            using (SqlConnection connection = new SqlConnection(StaticFields.connectionString))
            {
                connection.Open();

                string tableName = dataTable.ViewName;

                if (!string.IsNullOrEmpty(tableName))
                {
                    if (!string.IsNullOrEmpty(tableName))
                    {
                        string query = $"SELECT * FROM {tableName}";

                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection))
                        {
                            dataAdapter.Fill(dataTable);
                        }
                    }
                    else
                    {
                        throw new ArgumentNullException("Table name is null or empty.");
                    }
                }
                else
                {
                    throw new ArgumentException("TName property not found.");
                }
            }
        }

        public void FetchById(int id,BaseDataTable dataTable)
        {
            using (SqlConnection connection = new SqlConnection(StaticFields.connectionString))
            {
                connection.Open();

                string tableName = dataTable.ViewName;

                if (!string.IsNullOrEmpty(tableName))
                {
                    string query = $"SELECT * FROM {tableName} WHERE Id = @Id";

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection))
                    {
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@Id", id);
                        dataAdapter.Fill(dataTable);
                    }
                }
                else
                {
                    throw new ArgumentNullException("Table name is null or empty.");
                }
            }
        }


        public void Save(BaseDataTable dataTable)
        {
            using (SqlConnection connection = new SqlConnection(StaticFields.connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        SqlDataAdapter dataAdapter = CreateDataAdapter(dataTable, connection, transaction);
                        dataAdapter.Update(dataTable);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public void Delete(int id, BaseDataTable dataTable)
        {
            using (SqlConnection connection = new SqlConnection(StaticFields.connectionString))
            {
                connection.Open();

                string tableName = dataTable.TableName;

                if (!string.IsNullOrEmpty(tableName))
                {
                    using (SqlDataAdapter dataAdapter = CreateDataAdapter(dataTable, connection, null))
                    {
                        DataRow rowToDelete = null;
                        foreach (IdDataRow row in dataTable.Rows)
                        {
                            if (Convert.ToInt32(row.ID) == id)
                            {
                                rowToDelete = row;
                                break;
                            }
                        }

                        if (rowToDelete != null)
                        {
                            rowToDelete.Delete();
                            dataAdapter.Update(new DataRow[] { rowToDelete });
                        }
                        else
                        {
                            throw new InvalidOperationException(ErrorMessage.DataHasNotFound());
                        }
                    }
                }
                else
                {
                    throw new ArgumentNullException("Table name is null or empty.");
                }
            }
        }




        public void ExecuteQuery(string query, SqlParameter[] parameters, DataTable dataTable)
        {
            using (SqlConnection connection = new SqlConnection(StaticFields.connectionString))
            {
                connection.Open();

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection))
                {
                    if (parameters != null)
                    {
                        dataAdapter.SelectCommand.Parameters.AddRange(parameters);
                    }

                    dataAdapter.Fill(dataTable);
                }
            }
        }

        private SqlDataAdapter CreateDataAdapter(BaseDataTable dataTable, SqlConnection connection, SqlTransaction transaction)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            dataAdapter.SelectCommand = new SqlCommand($"SELECT * FROM {dataTable.TableName}", connection, transaction);
            dataAdapter.InsertCommand = GenerateInsertCommand(dataTable, connection, transaction);
            dataAdapter.UpdateCommand = GenerateUpdateCommand(dataTable, connection, transaction);
            dataAdapter.DeleteCommand = GenerateDeleteCommand(dataTable, connection, transaction);

            return dataAdapter;
        }

        private SqlCommand GenerateInsertCommand(BaseDataTable dataTable, SqlConnection connection, SqlTransaction transaction)
        {
            SqlCommand insertCommand = new SqlCommand($"INSERT INTO {dataTable.TableName} (", connection, transaction);

            foreach (DataColumn column in dataTable.Columns)
            {
                if (column.Caption != "Unserializable")
                {
                    insertCommand.CommandText += $"{column.ColumnName}, ";
                    insertCommand.Parameters.Add($"@{column.ColumnName}", column.DataType).SourceColumn = column.ColumnName;
                }
            }

            insertCommand.CommandText = insertCommand.CommandText.TrimEnd(',', ' ') + ") VALUES (";

            foreach (DataColumn column in dataTable.Columns)
            {
                if (column.Caption != "Unserializable")
                {
                    insertCommand.CommandText += $"@{column.ColumnName}, ";
                }
            }

            insertCommand.CommandText = insertCommand.CommandText.TrimEnd(',', ' ') + ")";

            foreach (DataColumn column in dataTable.Columns)
            {
                if (column.Caption != "Unserializable")
                {
                    insertCommand.Parameters[$"@{column.ColumnName}"].SourceVersion = DataRowVersion.Current;
                }
            }

            return insertCommand;
        }

        private SqlCommand GenerateUpdateCommand(BaseDataTable dataTable, SqlConnection connection, SqlTransaction transaction)
        {
            SqlCommand updateCommand = new SqlCommand($"UPDATE {dataTable.TableName} SET ", connection, transaction);

            foreach (DataColumn column in dataTable.Columns)
            {
                if (column.ColumnName != "Id" && column.Caption != "Unserializable")
                {
                    updateCommand.CommandText += $"{column.ColumnName} = @{column.ColumnName}, ";
                    updateCommand.Parameters.Add($"@{column.ColumnName}", column.DataType).SourceColumn = column.ColumnName;
                }
            }

            updateCommand.CommandText = updateCommand.CommandText.TrimEnd(',', ' ');

            foreach (DataColumn column in dataTable.Columns)
            {
                if (column.ColumnName != "Id" && column.Caption != "Unserializable")
                {
                    updateCommand.Parameters[$"@{column.ColumnName}"].SourceVersion = DataRowVersion.Current;
                }
            }

            updateCommand.CommandText += $" WHERE Id = @Id";
            updateCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");

            return updateCommand;
        }

        private SqlCommand GenerateDeleteCommand(BaseDataTable dataTable, SqlConnection connection, SqlTransaction transaction)
        {
            SqlCommand deleteCommand = new SqlCommand($"DELETE FROM {dataTable.TableName} WHERE Id = @Id", connection, transaction);
            deleteCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");

            return deleteCommand;
        }
    }

}