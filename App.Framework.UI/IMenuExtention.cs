﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Warehouse.Framework
{
    public interface IMenuExtension
    {
        string Name { get; }
        int Order { get; }
    }
}