using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Utils
{
    public class DateTimeUtil
    {
        public static DateTime DateGetDefaultFromDate()
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        }

        public static DateTime DateGetDefaultToDate()
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 11);
        }
    }
}