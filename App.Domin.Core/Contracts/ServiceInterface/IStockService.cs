using App.Framework;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace App.Domin.Core.Contracts.ServiceInterface
{
    public interface IStockService : IBaseService , IGenericService<StockDataSet>
    {
        void Save(int id,string name, string code);
        //void Save(StockDataSet stockDataSet);
        DataTable GetStockItemQuantityReport();
    }
}
