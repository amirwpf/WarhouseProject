using App.Framework;
using System;
using System.ComponentModel;
using System.Data;


namespace Core.Entites
{
    public class DeliveryTable : MasterDataTable<DeliveryRow>
    {
        public override string TableName { get; set; } = "delivery";
        public override string ViewName { get; set; } = "deliveryStock";

        public DeliveryTable()
        {
            Columns.Add(new DataColumn("Id", typeof(int)));
            Columns.Add(new DataColumn("Number", typeof(int)));
            Columns.Add(new DataColumn("StockId", typeof(int)));
            Columns.Add(new DataColumn("Date", typeof(DateTime)));
            Columns.Add(new DataColumn("StockCode", typeof(int)));
            Columns.Add(new DataColumn("StockName", typeof(string)));

            Columns["StockName"].Caption = "Unserializable";
            Columns["StockCode"].Caption = "Unserializable";
        }
    }

    public class DeliveryRow : DataRow
    {
        public DeliveryRow(DataRowBuilder builder) : base(builder)
        {
        }
        public int Id
        {
            get { return (int)this["Id"]; }
            set { this["Id"] = value; }
        }
        [DisplayName("شماره")]
        public int Number
        {
            get { return (int)this["Number"]; }
            set { this["Number"] = value; }
        }

        public int StockId
        {
            get { return (int)this["StockId"]; }
            set { this["StockId"] = value; }
        }
        [DisplayName("تاریخ")]
        public DateTime Date
        {
            get { return (DateTime)this["Date"]; }
            set { this["Date"] = value; }
        }
        [DisplayName("کد انبار")]
        public int StockCode
        {
            get { return (int)this["StockCode"]; }
            set { this["StockCode"] = value; }
        }
        [DisplayName("انبار")]
        public string StockName
        {
            get { return (string)this["StockName"]; }
            set { this["StockName"] = value; }
        }
    }
}