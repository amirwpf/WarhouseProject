using App.Framework.Entities.DataRows;
using System;
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
        }

        public void Delete(int id)
        {
            TDataSet ds = new TDataSet();
            _repository.Delete(id, ds.MasterTable);
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