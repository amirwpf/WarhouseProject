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

            _repository.FetchById(masterId ,ds.MasterTable);
            GetDetailByMasterId(masterId,ds);

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
            _repository.Delete(masterId,ds.MasterTable);
        }



        private void GetDetailByMasterId(int masterId, TDataSet dataSet)
        {
            var tableName = dataSet.DetailTable.TableName;
            var tableForeignKeyColumnName = dataSet.ForeignKeyColumnName;

            string query = $"SELECT * FROM {tableName} WHERE {tableForeignKeyColumnName} = @Id";
            SqlParameter[] parameters = { new SqlParameter("@Id", masterId) };

            _repository.ExecuteQuery(query, parameters, dataSet.DetailTable);
        }
    }

}