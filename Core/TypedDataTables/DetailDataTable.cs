using System.Data;

public abstract class DetailDataTable<T> : BaseTypedDataTable<T> where T : DataRow
{
    public virtual string ForeignKeyColumnName { get; set; } = "";
}
