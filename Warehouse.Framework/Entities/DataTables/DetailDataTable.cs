using System.Data;


namespace App.Framework
{
    public abstract class DetailDataTable<T> : BaseTypedDataTable<T> where T : DataRow
    {
        public virtual string ForeignKeyColumnName { get; set; } = "";
    }
}