using App.Framework.Entities.DataRows;

namespace App.Framework
{
    public abstract class MasterTypedDataTable<T> : BaseTypedDataTable<T> where T : IdDataRow
    {

    }
}