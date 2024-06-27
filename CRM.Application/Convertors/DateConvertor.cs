using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.Convertors
{
    public static class DateConvertor
    {
        public static string ToShamsiDate(this DateTime time)
        {
            var persianCalendar = new PersianCalendar();
            return persianCalendar.GetYear(time) + "/" +
                   persianCalendar.GetMonth(time).ToString("00") + "/" +
                   persianCalendar.GetDayOfMonth(time).ToString("00");

        }
    }
}
