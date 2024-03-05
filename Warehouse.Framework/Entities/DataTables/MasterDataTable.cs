using App.Framework.Entities.DataRows;
using System.Data;

namespace App.Framework
{
    public abstract class MasterDataTable<T> : BaseTypedDataTable<T> where T : IdDataRow
    {

    }
}