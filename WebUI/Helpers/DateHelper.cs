using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Helpers
{
    public static class DateHelper
    {
        public static string SMSDateFormat(DateTime date)
        {
            return date.ToString("MMM dd yyyy");
        }
    }
}