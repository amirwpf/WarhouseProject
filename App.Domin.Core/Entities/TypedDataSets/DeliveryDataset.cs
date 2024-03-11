using App.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Core.Entites
{


    public class DeliveryDataset : MasterDetailDataSet
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

        public override string ForeignKeyColumnName { get => "DeliveryId"; }

        public override BaseDataTable DetailTable => DeliveryItemsTable;

        public override BaseDataTable MasterTable => DeliveryTable;
    }
}
