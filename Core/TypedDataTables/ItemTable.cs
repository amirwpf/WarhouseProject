﻿using System.Data;

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

public class ItemRow : DataRow
{
    public ItemRow(DataRowBuilder builder) : base(builder)
    {
    }

    public int Id
    {
        get { return (int)this["Id"]; }
        set { this["Id"] = value; }
    }

    public int Code
    {
        get { return (int)this["Code"]; }
        set { this["Code"] = value; }
    }

    public string Name
    {
        get { return (string)this["Name"]; }
        set { this["Name"] = value; }
    }
}
