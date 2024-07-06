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

        public static DateTime ToMiladiDate(this string date)
        {
            var splitedDate = date.Split('/');

            int year = int.Parse(splitedDate[0]);
            int month = int.Parse(splitedDate[1]);
            int day = int.Parse(splitedDate[2]);

            DateTime thisDate = new DateTime(year, month, day, new PersianCalendar());

            return thisDate;
        }

    }
}
