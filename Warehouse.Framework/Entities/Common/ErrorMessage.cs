using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Framework
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

        public static string ValueMustBePositive(string fieldName)
        {
            return $"- {fieldName} باید عدد بزرگ تر از صفر باشد \n";
        }

        public static string ItemCantBeDeleted(string fieldName)
        {
            return $"- {fieldName} را نمی توان حذف کرد \n";
        }

        public static string RepititiveValue(string fieldName)
        {
            return $"- مقدار {fieldName} تکراری می باشد  \n";
        }

        public static string DataHasBeenChangedByOtherTransaction()
        {
            return "اطلاعات مورد نظر تغییر داده شده است. لطفا مجدد صفحه را بارگذاری کنید ";
        }

    }
}
