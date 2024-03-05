

namespace App.Framework
{
    public interface ITableIdService : IBaseService
    {
        int GetId(string tableName);
    }
}
