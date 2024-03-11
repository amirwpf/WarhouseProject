using App.Framework.Entities.DataRows;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace App.Framework
{
    public abstract class MasterDetailDAO<TDataSet>
        where TDataSet : MasterDetailDataSet, new()
    {
        protected readonly Repository _repository;

        public MasterDetailDAO()
        {
            _repository = new Repository();
        }

        public TDataSet GetMasterAll()
        {
            TDataSet ds = new TDataSet();
            _repository.FetchAll(ds.MasterTable);
            return ds;
        }

        public TDataSet GetAll()
        {
            TDataSet ds = new TDataSet();
            _repository.FetchAll(ds.MasterTable);
            _repository.FetchAll(ds.DetailTable);
            return ds;
        }

        public TDataSet GetMasterDetailByMasterId(int masterId)
        {
            TDataSet ds = new TDataSet();

            _repository.FetchById(masterId, ds.MasterTable);
            GetDetailByMasterId(masterId, ds);

            return ds;
        }

        public void SaveMasterDetail(TDataSet dataSet)
        {
            using (SqlConnection connection = new SqlConnection(StaticFields.connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {

                        if (dataSet.MasterTable.Rows.Count > 0)
                        {
                            int originalVersion = GetVersionFromDatabase((int)dataSet.MasterTable.Rows[0]["Id"], dataSet.MasterTable);
                            int currentVersion = (int)dataSet.MasterTable.Rows[0]["Version"];

                            if (currentVersion != originalVersion)
                            {
                                throw new InvalidOperationException(ErrorMessage.DataHasBeenChangedByOtherTransaction());
                            }
                            else
                            {
                                dataSet.MasterTable.Rows[0]["Version"] = currentVersion + 1;
                            }
                        }

                        _repository.Save(dataSet.MasterTable);
                        _repository.Save(dataSet.DetailTable);

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

        public void DeleteMasterDetailByMasterId(int masterId)
        {
            TDataSet ds = new TDataSet();
            _repository.Delete(masterId, ds.MasterTable);
        }

        private void GetDetailByMasterId(int masterId, TDataSet dataSet)
        {
            var tableName = dataSet.DetailTable.TableName;
            var tableForeignKeyColumnName = dataSet.ForeignKeyColumnName;

            string query = $"SELECT * FROM {tableName} WHERE {tableForeignKeyColumnName} = @Id";
            SqlParameter[] parameters = { new SqlParameter("@Id", masterId) };

            _repository.ExecuteQuery(query, parameters, dataSet.DetailTable);
        }

        public int GetVersionFromDatabase(int id, BaseDataTable dataTable)
        {
            var tableName = dataTable.ViewName;
            string query = $"SELECT Version FROM {tableName} WHERE Id = @Id";
            SqlParameter[] parameters = { new SqlParameter("@Id", id) };
            var newDataTable = (BaseDataTable)Activator.CreateInstance(dataTable.GetType());
            _repository.ExecuteQuery(query, parameters, newDataTable);
            return (int)newDataTable.Rows[0]["Version"];
        }
    }
}