using App.Framework;
using Core.Entites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace App.Domin.Core.Contracts.ServiceInterface
{
    public interface IDeliveryService : IBaseService,IGenericService<DeliveryDataset>
    {
        //DeliveryDataset GetByMasterId(int deliveryId);
        void Save(DeliveryDataset deliveryDataset);//, object selectedItem, string deliveryNumberText, DateTime deliveryDate);

        //void Save(DeliveryDataset deliveryDataset);
        //void DeleteById(int deliveryId);

    }
}
