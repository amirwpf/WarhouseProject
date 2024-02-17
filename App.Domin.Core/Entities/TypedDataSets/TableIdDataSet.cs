namespace Core.Entites
{
    public class TableIdDataSet : NonMasterDetailDataset<TableIdTable, TableIdRow>
    {
        public TableIdDataSet()
        {
            TableIdTable = new TableIdTable();
        }

        public TableIdTable TableIdTable { get; set; }

        public override TableIdTable MasterTable
        {
            get => TableIdTable;
            set => TableIdTable = value;
        }
    }
}

