using App.Framework.Entities.DataRows;
using System;
using System.Data;

namespace App.Framework
{
    public abstract class MasterDetailDataSet : BaseDataSet,IDetailDataTable
    {
        public abstract BaseDataTable DetailTable { get; }

        public abstract string ForeignKeyColumnName { get; }
    }
}
