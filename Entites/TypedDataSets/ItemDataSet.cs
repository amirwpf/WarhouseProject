public class ItemDataSet : NonMasterDetailDataset<ItemTable, ItemRow>
{
    public ItemDataSet()
    {
        ItemTable = new ItemTable();
    }

    public ItemTable ItemTable { get; set; }

    public override ItemTable MasterTable
    {
        get => ItemTable;
        set => ItemTable = value;
    }
}

