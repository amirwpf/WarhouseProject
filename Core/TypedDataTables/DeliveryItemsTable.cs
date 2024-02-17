using System.Data;

public class DeliveryItemsTable : DetailDataTable<DeliveryItemsRow>
{
    public override string ForeignKeyColumnName { get; set; } = "DeliveryId";
    public override string TableName { get; set; } = "deliveryItems";
    public override string ViewName { get; set; } = "deliveryItems";

    public DeliveryItemsTable()
    {
        Columns.Add(new DataColumn("Id", typeof(int)));
        Columns.Add(new DataColumn("ItemId", typeof(int)));
        Columns.Add(new DataColumn("DeliveryId", typeof(int)));
        Columns.Add(new DataColumn("Quantity", typeof(int)));
    }
}

public class DeliveryItemsRow : DataRow
{
    public DeliveryItemsRow(DataRowBuilder builder) : base(builder)
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

    public int DeliveryId
    {
        get { return (int)this["DeliveryId"]; }
        set { this["DeliveryId"] = value; }
    }


    public int Quantity
    {
        get { return (int)this["quantity"]; }
        set { this["quantity"] = value; }
    }
}