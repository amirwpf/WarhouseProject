﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Entites.Contracts.ServiceInterface
{
    internal interface IDeliveryService
    {
        DeliveryDataset GetByMasterId(int deliveryId);
        DeliveryDataset GetMasterAll();
        void Save(DeliveryDataset deliveryDataset, object selectedItem, string deliveryNumberText, DateTime deliveryDate);
        void Save(DeliveryDataset deliveryDataset);
        void DeleteById(int deliveryId);

    }
}
