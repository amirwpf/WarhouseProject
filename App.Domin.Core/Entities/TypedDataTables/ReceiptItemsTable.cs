﻿using App.Framework;
using App.Framework.Entities.DataRows;
using System.Data;


namespace Core.Entites
{
    public class ReceiptItemsTable : BaseTypedDataTable<ReceiptItemsRow>
    {

        public override string TableName { get=> "receiptItem"; }
        public override string ViewName { get=> "receiptItem"; } 

        public ReceiptItemsTable()
        {
            Columns.Add(new DataColumn("Id", typeof(int)));
            Columns.Add(new DataColumn("ItemId", typeof(int)));
            Columns.Add(new DataColumn("ReceiptId", typeof(int)));
            Columns.Add(new DataColumn("Quantity", typeof(int)));
        }
    }

    public class ReceiptItemsRow : IdDataRow
    {
        public ReceiptItemsRow(DataRowBuilder builder) : base(builder)
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

        public int ReceiptId
        {
            get { return (int)this["ReceiptId"]; }
            set { this["ReceiptId"] = value; }
        }


        public int Quantity
        {
            get { return (int)this["quantity"]; }
            set { this["quantity"] = value; }
        }
    }
}