using App.Framework.Entities.DataRows;
using System.Data;
using System.Data.SqlClient;

namespace App.Framework
{
    public abstract class NonMasterDetailDAO<TDataSet, TMaster, TMasterRow>
    where TDataSet : BaseDataSet<TMaster, TMasterRow>, new()
    where TMaster : MasterDataTable<TMasterRow>, new()
    where TMasterRow : IdDataRow
    {
        protected readonly GenericRepository<TMaster, TMasterRow> repository;

        public NonMasterDetailDAO(GenericRepository<TMaster, TMasterRow> repository)
        {
            this.repository = repository;
        }

        public TDataSet GetAll()
        {
            TDataSet ds = new TDataSet();
            ds.MasterTable = repository.GetAll();
            return ds;
        }

        public TDataSet GetById(int id)
        {
            TDataSet ds = new TDataSet();
            ds.MasterTable = repository.GetById(id);
            return ds;
        }

        public void Save(TDataSet dataSet)
        {
            repository.Save(dataSet.MasterTable);
        }

        protected void ExecuteQuery(string query, SqlParameter[] parameters, DataTable dataTable)
        {
            repository.ExecuteReportQuery(query, parameters, dataTable);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}