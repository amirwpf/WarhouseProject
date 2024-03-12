using App.Framework;
using App.Framework.Entities.DataRows;
using System;
using System.ComponentModel;
using System.Data;


namespace Core.Entites
{
    public class ReceiptTable : MasterTypedDataTable<ReceiptRow>
    {
        public override string TableName { get=> "receipt"; } 
        public override string ViewName { get => "receiptStock"; }

        public ReceiptTable()
        {
            Columns.Add(new DataColumn("Id", typeof(int)));
            Columns.Add(new DataColumn("Number", typeof(int)));
            Columns.Add(new DataColumn("StockId", typeof(int)));
            Columns.Add(new DataColumn("Date", typeof(DateTime)));
            Columns.Add(new DataColumn("StockCode", typeof(int)));
            Columns.Add(new DataColumn("StockName", typeof(string)));
            Columns.Add(new DataColumn("Version", typeof(int)));

            Columns["StockName"].Caption = "Unserializable";
            Columns["StockCode"].Caption = "Unserializable";
        }
    }
    public class ReceiptRow : IdDataRow, IVersionDataRow
    {
        public ReceiptRow(DataRowBuilder builder) : base(builder)
        {
        }
        public override int ID { get => Id; set => Id = value; }
        public int Id
        {
            get { return (int)this["Id"]; }
            set { this["Id"] = value; }
        }
        [DisplayName("شماره")]
        public int Number
        {
            get { if (this["Number"] != DBNull.Value) return (int)this["Number"]; else return 0; }
            set { this["Number"] = value; }
        }

        public int StockId
        {
            get { if (this["StockId"] != DBNull.Value) return (int)this["StockId"]; else return 0; }
            set { this["StockId"] = value; }
        }
        [DisplayName("تاریخ")]
        public DateTime Date
        {
            get { if (this["Date"] != DBNull.Value) return (DateTime)this["Date"]; else return DateTime.Now; }
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
        public int Version
        {
            get { if (this["Version"] != DBNull.Value) return (int)this["Version"]; else return 0; }
            set { this["Version"] = value; }
        }
    }
}