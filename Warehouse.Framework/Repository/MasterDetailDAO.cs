using App.Framework.Entities.DataRows;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace App.Framework
{
    public abstract class MasterDetailDAO<TDataSet> : BaseDAO<TDataSet>
        where TDataSet : MasterDetailDataSet, new()
    {
        #region ctor
        public MasterDetailDAO():base()
        {
        }
        #endregion

        #region CRUD

        public TDataSet GetMasterDetailById(int masterId)
        {
            TDataSet ds = new TDataSet();
            _repository.FetchById(masterId, ds.MasterTable);
            GetDetailByMasterId(masterId, ds);

            return ds;
        }

        public override void Save(TDataSet dataSet)
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
                            CheckVersion(dataSet, (IdDataRow)dataSet.MasterTable.Rows[0]);
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
        #endregion

        #region other methods
        private void GetDetailByMasterId(int masterId, TDataSet dataSet)
        {
            var tableName = dataSet.DetailTable.TableName;
            var tableForeignKeyColumnName = dataSet.ForeignKeyColumnName;

            string query = $"SELECT * FROM {tableName} WHERE {tableForeignKeyColumnName} = @Id";
            SqlParameter[] parameters = { new SqlParameter("@Id", masterId) };

            _repository.ExecuteQuery(query, parameters, dataSet.DetailTable);
        }
        #endregion
    }
}