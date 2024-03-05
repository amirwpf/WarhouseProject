using App.Framework.Entities.DataRows;
using System.Data;


namespace App.Framework
{
    public abstract class DetailDataTable<T> : BaseTypedDataTable<T> where T : IdDataRow
    {
        public virtual string ForeignKeyColumnName { get; set; } = "";
    }
}