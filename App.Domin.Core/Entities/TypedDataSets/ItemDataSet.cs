using System.Data;
using App.Framework;

namespace Core.Entites
{
    public class ItemDataSet : BaseDataSet
    {
        public ItemDataSet()
        {
            ItemTable = new ItemTable();
        }

        public ItemTable ItemTable { get; set; }

        public override BaseDataTable MasterTable { get => ItemTable;}
    }
}
