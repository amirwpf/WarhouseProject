﻿using App.Framework;
using System.ComponentModel;
using System.Data;


namespace Core.Entites
{
    public class StockTable : MasterDataTable<StockRow>
    {
        public override string TableName { get; set; } = "stock";
        public override string ViewName { get; set; } = "stock";

        public StockTable()
        {
            Columns.Add(new DataColumn("Id", typeof(int)));
            Columns.Add(new DataColumn("Code", typeof(int)));
            Columns.Add(new DataColumn("Name", typeof(string)));
        }
    }
    public class StockRow : DataRow
    {
        public StockRow(DataRowBuilder builder) : base(builder)
        {
        }

        public int Id
        {
            get { return (int)this["Id"]; }
            set { this["Id"] = value; }
        }
        [DisplayName("کد")]
        public int Code
        {
            get { return (int)this["Code"]; }
            set { this["Code"] = value; }
        }
        [DisplayName("نام")]
        public string Name
        {
            get { return (string)this["Name"]; }
            set { this["Name"] = value; }
        }
    }

}