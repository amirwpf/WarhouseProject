using App.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Domin.Core.Contracts.ServiceInterface
{
    public interface ITableIdService : IBaseService
    {
        int GetId(DbTablesEnum DbTablesEnum);
    }
}
