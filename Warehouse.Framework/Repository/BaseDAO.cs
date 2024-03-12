using App.Framework.Entities.DataRows;
using System;
using System.Data;
using System.Data.SqlClient;

namespace App.Framework
{
    public abstract class BaseDAO<TDataSet> where TDataSet : BaseDataSet, new()
    {

        #region ctor
        protected readonly Repository _repository;

        public BaseDAO()
        {
            _repository = new Repository();
        }
        #endregion


        #region CRUD
        public virtual TDataSet GetAll()
        {
            TDataSet ds = new TDataSet();
            _repository.FetchAll(ds.MasterTable);
            return ds;
        }

        public virtual TDataSet GetById(int id)
        {
            TDataSet ds = new TDataSet();
            _repository.FetchById(id, ds.MasterTable);
            return ds;
        }

        public virtual void Save(TDataSet dataSet)
        {
            if (dataSet.MasterTable.Rows.Count > 0)
            {
                CheckVersion(dataSet);
            }

            _repository.Save(dataSet.MasterTable);
        }

        public virtual void Delete(int id)
        {
            TDataSet ds = new TDataSet();
            _repository.Delete(id, ds.MasterTable);
        }

        #endregion


        #region other methods
        protected void CheckVersion(TDataSet dataSet)
        {
            if (!(dataSet.MasterTable.Rows[0] is IVersionDataRow versionDataRow))
            {
                return;
            }

            int id = GetRowId(dataSet.MasterTable.Rows[0]);
            int originalVersion = GetOriginalVersion(id, dataSet.MasterTable);
            int currentVersion = versionDataRow.Version;

            ValidateVersions(originalVersion, currentVersion);
            UpdateVersion(versionDataRow, currentVersion);
        }

        private int GetRowId(DataRow row)
        {
            return (row as IdDataRow)?.ID ?? 0;
        }

        private int GetOriginalVersion(int id, BaseDataTable dataTable)
        {
            if (id == 0)
            {
                throw new InvalidOperationException(ErrorMessage.DataHasNotFound());
            }

            return GetVersionFromDatabase(id, dataTable);
        }

        private void ValidateVersions(int originalVersion, int currentVersion)
        {
            if (currentVersion != originalVersion)
            {
                throw new InvalidOperationException(ErrorMessage.DataHasBeenChangedByOtherTransaction());
            }
        }

        private void UpdateVersion(IVersionDataRow versionDataRow, int currentVersion)
        {
            versionDataRow.Version = currentVersion + 1;
        }




        public int GetVersionFromDatabase(int id, BaseDataTable dataTable)
        {
            var tableName = dataTable.ViewName;
            string query = $"SELECT Version FROM {tableName} WHERE Id = @Id";
            SqlParameter[] parameters = { new SqlParameter("@Id", id) };
            var newDataTable = (BaseDataTable)Activator.CreateInstance(dataTable.GetType());
            _repository.ExecuteQuery(query, parameters, newDataTable);
            if (newDataTable.Rows.Count > 0)
                return ((IVersionDataRow)newDataTable.Rows[0]).Version;
            else
                return 0;
        }

        #endregion
    }
}