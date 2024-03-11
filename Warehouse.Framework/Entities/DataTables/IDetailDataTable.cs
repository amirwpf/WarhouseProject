using App.Framework.Entities.DataRows;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;


namespace App.Framework
{
    public interface IDetailDataTable
    {
        string ForeignKeyColumnName { get; }
    }
}