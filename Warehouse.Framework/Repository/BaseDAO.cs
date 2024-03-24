using App.Framework.Entities.DataRows;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace App.Framework
{
    public abstract class BaseDAO<TDataSet> where TDataSet : BaseDataSet, new()
    {

        #region ctor
        protected readonly Repository _repository;

        public BaseDAO()
        {
            _repository = Repository.GetRepository();
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
            if (dataSet.MasterTable.Rows.Count > 0 /*&& dataSet.MasterTable.Rows[0].RowState!=DataRowState.Deleted*/)
            {
                CheckVersion(dataSet,(IdDataRow)dataSet.MasterTable.Rows[0]);
            }

            _repository.Save(dataSet.MasterTable);
        }

        public virtual void Delete(int id)
        {
            TDataSet ds = new TDataSet();
            _repository.Delete(id, ds.MasterTable);
        }

        public virtual void DeleteWithcheckVersion(TDataSet ds, IdDataRow row)
        {
            if (ds.MasterTable.Rows.Count > 0)
            {
                CheckVersion(ds,row);
            }

            _repository.Save(ds.MasterTable);
        }
        #endregion


        #region other methods
        protected void CheckVersion(TDataSet dataSet, IdDataRow row)
        {
            int originalIdVersion = 0;

            if(row.RowState==DataRowState.Deleted)
            {
                originalIdVersion = (int)(row.GetOriginalPropertyValue(nameof(row.ID)));
            }
            else
                originalIdVersion = GetRowId(dataSet.MasterTable.Rows[0]);

            if (row is IVersionDataRow versionDataRow)
            {
                int originalVersionVersion = 0;
                if (row.RowState == DataRowState.Deleted)
                    originalVersionVersion = (int)(row.GetOriginalPropertyValue(nameof(versionDataRow.Version)));
                else
                    originalVersionVersion = versionDataRow.Version;


                int originalVersion = GetOriginalVersion(originalIdVersion, dataSet.MasterTable);
                int currentVersion = originalVersionVersion;

                ValidateVersions(originalVersion, currentVersion);
                if(row.RowState!=DataRowState.Deleted)
                    UpdateVersion(versionDataRow, currentVersion);
            }
            else return;


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