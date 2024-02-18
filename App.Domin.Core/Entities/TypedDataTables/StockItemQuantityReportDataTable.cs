using Core.Entites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace App.Domin.Core.Entities.TypedDataTables
{
    public class StockItemQuantityReportDataTable : MasterDataTable<StockItemQuantityReportRow>
    {
        public override string TableName { get; set; } = "Stock_Item_Quantity_View";
        public override string ViewName { get; set; } = "Stock_Item_Quantity_View";

        public StockItemQuantityReportDataTable()
        {
            Columns.Add(new DataColumn("Id", typeof(int)));
            Columns.Add(new DataColumn("Name", typeof(string)));
            Columns.Add(new DataColumn("Quantity", typeof(int)));
        }
    }

    public class StockItemQuantityReportRow : DataRow
    {
        public StockItemQuantityReportRow(DataRowBuilder builder) : base(builder)
        {
        }

        public int Id
        {
            get { return (int)this["Id"]; }
            set { this["Id"] = value; }
        }
        public string Name
        {
            get { return (string)this["Name"]; }
            set { this["Name"] = value; }
        }

        public int Quantity
        {
            get { return (int)this["Quantity"]; }
            set { this["Quantity"] = value; }
        }


    }
}
