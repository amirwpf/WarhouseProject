using System.Data;


namespace Core.Entites
{
    public abstract class DetailDataTable<T> : BaseTypedDataTable<T> where T : DataRow
    {
        public virtual string ForeignKeyColumnName { get; set; } = "";
    }
}