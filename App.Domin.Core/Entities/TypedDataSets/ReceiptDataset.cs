using Core.Entites;

namespace Core.Entites
{
    public class ReceiptDataset : MasterDetailDataset<ReceiptTable, ReceiptItemsTable, ReceiptRow, ReceiptItemsRow>
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


        public override ReceiptTable MasterTable
        {
            get => ReceiptTable;
            set => ReceiptTable = value;
        }
        public override ReceiptItemsTable DetailTable
        {
            get => ReceiptItemsTable;
            set => ReceiptItemsTable = value;
        }
    }
}
