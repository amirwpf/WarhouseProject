using App.Framework;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Domin.Core.Contracts.ServiceInterface
{
    public interface IItemService : IBaseService
    {
        ItemDataSet GetById(int itemId);
        ItemDataSet GetAll();

        void Save(int id,string name, string code);


        //void Save(ItemDataSet itemDataSet);


        void DeleteById(int itemId);

    }
}
