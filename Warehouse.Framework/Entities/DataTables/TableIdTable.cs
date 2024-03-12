using App.Framework;
using App.Framework.Entities.DataRows;
using System.Data;


namespace App.Framework
{
    public class TableIdTable : MasterTypedDataTable<TableIdRow>
    {
        public override string TableName { get=> "tableIds"; }
        public override string ViewName { get=> "tableIds"; }

        public TableIdTable()
        {
            Columns.Add(new DataColumn("Id", typeof(int)));
            Columns.Add(new DataColumn("TableName", typeof(string)));
            Columns.Add(new DataColumn("IdNumber", typeof(int)));
        }
    }

    public class TableIdRow : IdDataRow
    {
        public TableIdRow(DataRowBuilder builder) : base(builder)
        {
        }

        public override int ID { get => Id; set => Id = value; }

        public int Id
        {
            get { return (int)this["Id"]; }
            set { this["Id"] = value; }
        }
        public int IdNumber
        {
            get { return (int)this["IdNumber"]; }
            set { this["IdNumber"] = value; }
        }
        public string TableName
        {
            get { return (string)this["TableName"]; }
            set { this["TableName"] = value; }
        }
    }
}