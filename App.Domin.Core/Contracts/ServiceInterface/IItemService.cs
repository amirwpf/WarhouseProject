using App.Framework;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Domin.Core.Contracts.ServiceInterface
{
    public interface IItemService : IEntityService<ItemDataSet>
    {
        void Save(ItemDataSet itemDataSet);
    }
}
