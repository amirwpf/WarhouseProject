using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework.UI
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExtentionMenuAttribute : Attribute
    {
        public string CategoryName { get; set; }
        public string MenuName { get; set; }
        public int Order { get; set; }
    }
}
