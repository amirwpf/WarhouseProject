using System;
using System.Data;

namespace App.Framework
{
    public abstract class BaseDataSet : DataSet
    {
        public abstract BaseDataTable MasterTable { get;}
    }
}