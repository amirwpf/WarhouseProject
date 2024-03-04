using System;

namespace App.Framework.UI
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExtentionMenuListAttribute : Attribute
    {
        public string CategoryName { get; set; }
    }
}
