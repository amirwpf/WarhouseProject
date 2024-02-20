using App.Framework;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace App.Domin.Core.Contracts.ServiceInterface
{
    public interface IStockService : IBaseService
    {
        StockDataSet GetById(int itemId);
        StockDataSet GetAll();
        void Save(int id,string name, string code);
        //void Save(StockDataSet stockDataSet);

        void DeleteById(int itemId);
        DataTable GetStockItemQuantityReport();
    }
}
