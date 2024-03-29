﻿using App.Framework;
using App.Framework.Entities.DataRows;
using System.Data;


namespace Core.Entites
{
    public class DeliveryItemsTable : BaseTypedDataTable<DeliveryItemsRow>
    {

        public override string TableName { get=> "deliveryItems";  }
        public override string ViewName { get=> "deliveryItems";  } 

        public DeliveryItemsTable()
        {
            Columns.Add(new DataColumn("Id", typeof(int)));
            Columns.Add(new DataColumn("ItemId", typeof(int)));
            Columns.Add(new DataColumn("DeliveryId", typeof(int)));
            Columns.Add(new DataColumn("Quantity", typeof(int)));
        }
    }

    public class DeliveryItemsRow : IdDataRow
    {
        public DeliveryItemsRow(DataRowBuilder builder) : base(builder)
        {
        }
        public override int ID { get => Id; set => Id = value; }
        public int Id
        {
            get { return (int)this["id"]; }
            set { this["id"] = value; }
        }

        public int ItemId
        {
            get { return (int)this["ItemId"]; }
            set { this["ItemId"] = value; }
        }

        public int DeliveryId
        {
            get { return (int)this["DeliveryId"]; }
            set { this["DeliveryId"] = value; }
        }


        public int Quantity
        {
            get { return (int)this["quantity"]; }
            set { this["quantity"] = value; }
        }
    }

}