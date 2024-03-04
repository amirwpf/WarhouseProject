using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace App.Framework
{
    public interface IEntityService<TDataSet> :IBaseService where TDataSet : DataSet
    {
        TDataSet GetById(int id);
        TDataSet GetAll();
        void DeleteById(int id);
    }
}
