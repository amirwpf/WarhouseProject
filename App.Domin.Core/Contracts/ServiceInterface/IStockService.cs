using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Domin.Core.Contracts.ServiceInterface
{
    public interface IStockService
    {
        StockDataSet GetById(int itemId);
        StockDataSet GetAll();
        void Save(string name, string code);
        void Save(StockDataSet stockDataSet);

        void DeleteById(int itemId);
    }
}
