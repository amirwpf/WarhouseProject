using System.Data;


namespace Core.Entites
{
    public class TableIdTable : MasterDataTable<TableIdRow>
    {
        public override string TableName { get; set; } = "tableIds";
        public override string ViewName { get; set; } = "tableIds";

        public TableIdTable()
        {
            Columns.Add(new DataColumn("Id", typeof(int)));
            Columns.Add(new DataColumn("TableName", typeof(string)));
            Columns.Add(new DataColumn("IdNumber", typeof(int)));
        }
    }

    public class TableIdRow : DataRow
    {
        public TableIdRow(DataRowBuilder builder) : base(builder)
        {
        }

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