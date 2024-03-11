using App.Framework.Entities.DataRows;
using System.Data;
using System.Data.SqlClient;

namespace App.Framework
{
    public abstract class NonMasterDetailDAO<TDataSet> where TDataSet:BaseDataSet,new()
    {
        protected readonly Repository _repository;

        public NonMasterDetailDAO()
        {
            _repository = new Repository();
        }

        public TDataSet GetAll()
        {
            TDataSet ds = new TDataSet();
            _repository.FetchAll(ds.MasterTable);
            return ds;
        }

        public TDataSet GetById(int id)
        {
            TDataSet ds = new TDataSet();
            _repository.FetchById(id, ds.MasterTable);
            return ds;
        }

        public void Save(TDataSet dataSet)
        {
            _repository.Save(dataSet.MasterTable);
        }

        public void Delete(int id)
        {
            TDataSet ds = new TDataSet();
            _repository.Delete(id, ds.MasterTable);
        }
    }
}