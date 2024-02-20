using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Domin.Core
{
    public static class ErrorMessage
    {

        public static string InValidFieldValue(string fieldName)
        {
            return $"- مقدار {fieldName} نامعتبر است \n";
        }

        public static string ItemCantBeEmpty(string fieldName)
        {
            return $"- {fieldName} باید به درستی مقدار دهی شود \n";
        }

        public static string ItemCantBeDeleted(string fieldName)
        {
            return $"- {fieldName} را نمی توان حذف کرد \n";
        }

        public static string RepititiveValue(string fieldName)
        {
            return $"- مقدار {fieldName} تکراری می باشد  \n";
        }

    }
}
