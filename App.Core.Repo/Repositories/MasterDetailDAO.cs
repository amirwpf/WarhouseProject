using Core.Entites;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Warehouse.Framework.Common;

namespace Infrastructure.Repositories
{
    public abstract class MasterDetailDAO<TDataSet, TMaster, TDetail, TRowMaster, TRowDetail>
    where TDataSet : MasterDetailDataset<TMaster, TDetail, TRowMaster, TRowDetail>, new()
    where TMaster : MasterDataTable<TRowMaster>, new()
    where TRowMaster : DataRow
    where TDetail : DetailDataTable<TRowDetail>, new()
    where TRowDetail : DataRow
    {
        private readonly GenericRepository<TMaster, TRowMaster> masterRepository;
        private readonly GenericRepository<TDetail, TRowDetail> detailRepository;

        public MasterDetailDAO(GenericRepository<TMaster, TRowMaster> masterRepository, GenericRepository<TDetail, TRowDetail> detailRepository)
        {
            this.masterRepository = masterRepository;
            this.detailRepository = detailRepository;
        }

        public TDataSet GetMasterAll()
        {
            TDataSet ds = new TDataSet();
            ds.MasterTable = masterRepository.GetAll();
            return ds;
        }

        public TDataSet GetMasterDetailByMasterId(int masterId)
        {
            TDataSet dataSet = new TDataSet();

            dataSet.MasterTable = masterRepository.GetById(masterId);
            dataSet.DetailTable = GetDetailByMasterId(masterId);

            return dataSet;
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
                        masterRepository.Save(dataSet.MasterTable);
                        detailRepository.Save(dataSet.DetailTable);

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
            //ClearDetailByMasterId(masterId);
            masterRepository.Delete(masterId);
        }



        private TDetail GetDetailByMasterId(int masterId)
        {
            TDetail dataTable = new TDetail();
            var tableName = dataTable.TableName;
            var tableForeignKeyColumnName = dataTable.ForeignKeyColumnName;

            string query = $"SELECT * FROM {tableName} WHERE {tableForeignKeyColumnName} = @Id";
            SqlParameter[] parameters = { new SqlParameter("@Id", masterId) };

            detailRepository.ExecuteQuery(query, parameters, dataTable);

            return dataTable;
        }

        private TDetail ClearDetailByMasterId(int masterId)
        {
            TDetail dataTable = new TDetail();
            var tableName = dataTable.TableName;
            var tableForeignKeyColumnName = dataTable.ForeignKeyColumnName;

            string query = $"DELETE FROM {tableName} WHERE {tableForeignKeyColumnName} = @Id";
            SqlParameter[] parameters = { new SqlParameter("@Id", masterId) };

            detailRepository.ExecuteQuery(query, parameters, dataTable);

            return dataTable;
        }
    }

}