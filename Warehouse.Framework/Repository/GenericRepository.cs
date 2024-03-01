using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace App.Framework
{
    public class GenericRepository<T, Trow> where T : BaseTypedDataTable<Trow>, new()
                                                           where Trow : DataRow
    {
        public T GetAll()
        {
            using (SqlConnection connection = new SqlConnection(StaticFields.connectionString))
            {
                connection.Open();

                T dataTable = new T();
                string tableName = dataTable.ViewName;

                if (!string.IsNullOrEmpty(tableName))
                {
                    if (!string.IsNullOrEmpty(tableName))
                    {
                        string query = $"SELECT * FROM {tableName}";

                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection))
                        {
                            dataAdapter.Fill(dataTable);
                            return dataTable;
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

        public T GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(StaticFields.connectionString))
            {
                connection.Open();

                T dataTable = new T();
                string tableName = dataTable.ViewName;

                if (!string.IsNullOrEmpty(tableName))
                {
                    string query = $"SELECT * FROM {tableName} WHERE Id = @Id";

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection))
                    {
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@Id", id);
                        dataAdapter.Fill(dataTable);
                        return dataTable;
                    }
                }
                else
                {
                    throw new ArgumentNullException("Table name is null or empty.");
                }
            }
        }


        public void Save(T dataTable)
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

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(StaticFields.connectionString))
            {
                connection.Open();

                T dataTable = new T();
                string tableName = dataTable.TableName;


                if (!string.IsNullOrEmpty(tableName))
                {
                    string query = $"DELETE FROM {tableName} WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    throw new ArgumentNullException("Table name is null or empty.");
                }


            }
        }

        public void ExecuteQuery(string query, SqlParameter[] parameters, T dataTable)
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

        public void ExecuteReportQuery(string query, SqlParameter[] parameters, DataTable dataTable)
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

        private SqlDataAdapter CreateDataAdapter(T dataTable, SqlConnection connection, SqlTransaction transaction)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            dataAdapter.SelectCommand = new SqlCommand($"SELECT * FROM {dataTable.TableName}", connection, transaction);
            dataAdapter.InsertCommand = GenerateInsertCommand(dataTable, connection, transaction);
            dataAdapter.UpdateCommand = GenerateUpdateCommand(dataTable, connection, transaction);
            dataAdapter.DeleteCommand = GenerateDeleteCommand(dataTable, connection, transaction);

            return dataAdapter;
        }

        private SqlCommand GenerateInsertCommand(T dataTable, SqlConnection connection, SqlTransaction transaction)
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

        private SqlCommand GenerateUpdateCommand(T dataTable, SqlConnection connection, SqlTransaction transaction)
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

        private SqlCommand GenerateDeleteCommand(T dataTable, SqlConnection connection, SqlTransaction transaction)
        {
            SqlCommand deleteCommand = new SqlCommand($"DELETE FROM {dataTable.TableName} WHERE Id = @Id", connection, transaction);
            deleteCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");

            return deleteCommand;
        }
    }

}