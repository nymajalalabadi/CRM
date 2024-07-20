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

        public static string GetShamsiMonthName(this DateTime date)
        {
            var persianCalendar = new PersianCalendar();

            int intMonth = persianCalendar.GetMonth(date);

            var result = "";

            switch (intMonth)
            {
                case 1:
                    result = "فروردین";
                    break;
                case 2:
                    result = "اردیبهشت";
                    break;
                case 3:
                    result = "خرداد";
                    break;
                case 4:
                    result = "تیر";
                    break;
                case 5:
                    result = "مرداد";
                    break;
                case 6:
                    result = "شهریور";
                    break;
                case 7:
                    result = "مهر";
                    break;
                case 8:
                    result = "آبان";
                    break;
                case 9:
                    result = "آذر";
                    break;
                case 10:
                    result = "دی";
                    break;
                case 11:
                    result = "بهمن";
                    break;
                case 12:
                    result = "اسفند";
                    break;
                default:
                    result = "";
                    break;
            }

            return result;
        }
    }
}
