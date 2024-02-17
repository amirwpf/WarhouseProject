﻿using System;
using System.Data;

namespace Core.Entites
{
    public abstract class NonMasterDetailDataset<TMaster, TMasterRow> : DataSet
    where TMaster : MasterDataTable<TMasterRow>, new()
    where TMasterRow : DataRow
    {
        public NonMasterDetailDataset()
        {
            //MasterTable = Activator.CreateInstance<TMaster>();
        }
        public abstract TMaster MasterTable { get; set; }
    }
}