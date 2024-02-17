using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehouse.Framework.Common;

namespace App.Domin.Core.Contracts.ServiceInterface
{
    public interface ITableIdService
    {
        int GetId(DbTablesEnum DbTablesEnum);
    }
}
