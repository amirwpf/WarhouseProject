using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

public abstract class BaseTypedDataTable<T> : DataTable, IEnumerable<T> where T : DataRow
{
    public virtual string TableName { get; set; } = "";
    public virtual string ViewName { get; set; } = "";

    public BaseTypedDataTable()
    {
    }

    protected override Type GetRowType()
    {
        return typeof(T);
    }

    protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
    {
        Type itemType = typeof(T);
        return (T)Activator.CreateInstance(itemType, builder);
    }

    public T this[int idx]
    {
        get { return (T)Rows[idx]; }
    }

    public void Add(T row)
    {
        Rows.Add(row);
    }

    public void Remove(T row)
    {
        Rows.Remove(row);
    }

    public T GetNewRow()
    {
        DataRow row = NewRow();
        return (T)row;
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var row in Rows)
        {
            yield return (T)row;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
