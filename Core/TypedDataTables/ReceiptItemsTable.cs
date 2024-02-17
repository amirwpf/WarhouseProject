using System.Data;

public class ReceiptItemsTable : DetailDataTable<ReceiptItemsRow>
{
    public override string ForeignKeyColumnName { get; set; } = "ReceiptId";
    public override string TableName { get; set; } = "receiptItem";
    public override string ViewName { get; set; } = "receiptItem";

    public ReceiptItemsTable()
    {
        Columns.Add(new DataColumn("Id", typeof(int)));
        Columns.Add(new DataColumn("ItemId", typeof(int)));
        Columns.Add(new DataColumn("ReceiptId", typeof(int)));
        Columns.Add(new DataColumn("Quantity", typeof(int)));
    }
}

public class ReceiptItemsRow : DataRow
{
    public ReceiptItemsRow(DataRowBuilder builder) : base(builder)
    {
    }
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
