using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Domin.Core.Contracts.ServiceInterface
{
    public interface IItemService
    {
        ItemDataSet GetById(int itemId);
        ItemDataSet GetAll();

        void Save(string name, string code);


        void Save(ItemDataSet itemDataSet);


        void DeleteById(int itemId);

    }
}
