using System;

namespace App.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExtentionMenuListAttribute : Attribute
    {
        public string CategoryName { get; set; }
    }
}
