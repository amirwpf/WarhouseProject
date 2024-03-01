using System;
using System.Data;

namespace App.Framework
{
    public abstract class BaseDataSet<TMaster, TMasterRow> : DataSet
    where TMaster : MasterDataTable<TMasterRow>, new()
    where TMasterRow : DataRow
    {
        public BaseDataSet()
        {
            //MasterTable = Activator.CreateInstance<TMaster>();
        }
        public abstract TMaster MasterTable { get; set; }
    }
}