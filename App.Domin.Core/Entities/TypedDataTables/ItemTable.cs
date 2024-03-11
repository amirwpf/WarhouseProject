using App.Framework;
using App.Framework.Entities.DataRows;
using System;
using System.ComponentModel;
using System.Data;


namespace Core.Entites
{
    public class ItemTable : MasterTypedDataTable<ItemRow>
    {
        public override string TableName { get=> "item"; } 
        public override string ViewName { get=> "item"; } 
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
            get { if (this["Code"] != DBNull.Value) return (int)(this["Code"]); else return 0; }
            set { this["Code"] = value; }
        }
        [DisplayName("نام")]
        public string Name
        {
            get { if (this["Name"] != DBNull.Value) return (string)this["Name"]; else return ""; }
            set { this["Name"] = value; }
        }
    }
}