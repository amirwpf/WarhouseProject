using Core.Entites;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarehouseTest.Services.DeliveryService
{
    public class DeliveryServiceDAO : MasterDetailDAO<DeliveryDataset,DeliveryTable, DeliveryItemsTable, DeliveryRow, DeliveryItemsRow>
    {
        public DeliveryServiceDAO() : base(new GenericRepository<DeliveryTable, DeliveryRow>(), new GenericRepository<DeliveryItemsTable, DeliveryItemsRow>())
        {

        }
    }
}
