using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseTest.UI.models;

namespace Warehouse.Framework.Common
{
    public interface IExtension
    {
        string Name { get; }
        int Order { get; }
        BaseForm Btn_Click();
    }
}
