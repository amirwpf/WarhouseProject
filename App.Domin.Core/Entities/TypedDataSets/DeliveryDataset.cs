using App.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Core.Entites
{


    public class DeliveryDataset : MasterDetailDataset<DeliveryTable, DeliveryItemsTable, DeliveryRow, DeliveryItemsRow>
    {
        public DeliveryDataset() : base()
        {
            DeliveryTable = new DeliveryTable();
            DeliveryItemsTable = new DeliveryItemsTable();


            Tables.Add(DeliveryTable);
            Tables.Add(DeliveryItemsTable);
        }

        public DeliveryTable DeliveryTable { get; set; }
        public DeliveryItemsTable DeliveryItemsTable { get; set; }


        public override DeliveryTable MasterTable
        {
            get => DeliveryTable;
            set => DeliveryTable = value;
        }
        public override DeliveryItemsTable DetailTable
        {
            get => DeliveryItemsTable;
            set => DeliveryItemsTable = value;
        }
    }
}
