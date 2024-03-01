using App.Framework;

namespace Core.Entites
{
    public class ItemDataSet : BaseDataSet<ItemTable, ItemRow>
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
}
