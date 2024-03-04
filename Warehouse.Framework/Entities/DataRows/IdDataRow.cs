using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace App.Framework.Entities.DataRows
{
    public abstract class IdDataRow : DataRow
    {
        public IdDataRow(DataRowBuilder builder) : base(builder)
        {
        }

        public abstract int ID { set; get; }
    }
}
