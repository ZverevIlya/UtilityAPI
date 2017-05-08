using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helper.DateTimeHelper
{
    public static class DateTimeExtensions
    {
        public static string ToShortDateFileNameString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
    }
}
