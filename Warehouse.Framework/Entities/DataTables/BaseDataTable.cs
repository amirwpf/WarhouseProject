using System.Data;


namespace App.Framework
{
    public abstract class BaseDataTable : DataTable
    {
        public abstract string TableName { get; }
        public abstract string ViewName { get; }
    }
}