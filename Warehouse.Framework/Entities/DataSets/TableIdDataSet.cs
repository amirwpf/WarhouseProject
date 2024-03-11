using System.Data;
using App.Framework;

namespace App.Framework
{
    public class TableIdDataSet : BaseDataSet
    {
        public TableIdDataSet()
        {
            TableIdTable = new TableIdTable();
        }
        public TableIdTable TableIdTable { get; set; }

        public override BaseDataTable MasterTable => TableIdTable;
    }
}

