using System.Data;
using App.Framework;
using Core.Entites;

namespace Core.Entites
{
    public class ReceiptDataset : MasterDetailDataSet
    {
        public ReceiptDataset() : base()
        {
            ReceiptTable = new ReceiptTable();
            ReceiptItemsTable = new ReceiptItemsTable();


            Tables.Add(ReceiptTable);
            Tables.Add(ReceiptItemsTable);
        }

        public ReceiptTable ReceiptTable { get; set; }
        public ReceiptItemsTable ReceiptItemsTable { get; set; }

        public override BaseDataTable DetailTable => ReceiptItemsTable;

        public override BaseDataTable MasterTable => ReceiptTable;

        public override string ForeignKeyColumnName { get => "ReceiptId"; }
    }
}
