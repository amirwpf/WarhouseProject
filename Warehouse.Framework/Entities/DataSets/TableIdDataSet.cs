using App.Framework;

namespace App.Framework
{
    public class TableIdDataSet : BaseDataSet<TableIdTable, TableIdRow>
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

