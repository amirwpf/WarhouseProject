using App.Framework;
using App.Framework.Entities.DataRows;
using System.ComponentModel;
using System.Data;


namespace Core.Entites
{
    public class ItemTable : MasterDataTable<ItemRow>
    {
        public override string TableName { get; set; } = "item";
        public override string ViewName { get; set; } = "item";

        public ItemTable()
        {
            Columns.Add(new DataColumn("Id", typeof(int)));
            Columns.Add(new DataColumn("Code", typeof(int)));
            Columns.Add(new DataColumn("Name", typeof(string)));
        }
    }

    public class ItemRow : IdDataRow
    {
        public ItemRow(DataRowBuilder builder) : base(builder)
        {
        }

        public override int ID { get => Id; set => Id=value; }
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