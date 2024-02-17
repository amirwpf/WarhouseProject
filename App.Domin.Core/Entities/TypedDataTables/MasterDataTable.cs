using System.Data;

namespace Core.Entites
{
    public abstract class MasterDataTable<T> : BaseTypedDataTable<T> where T : DataRow
    {

    }
}