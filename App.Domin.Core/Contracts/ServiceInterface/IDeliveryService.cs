using App.Framework;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace App.Domin.Core.Contracts.ServiceInterface
{
    public interface IDeliveryService : IEntityService<DeliveryDataset>
    {
        DeliveryDataset GetByStockId(int stockId);
        DeliveryDataset GetByItemId(int itemId);

    }
}
