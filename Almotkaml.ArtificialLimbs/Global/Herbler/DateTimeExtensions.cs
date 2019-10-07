using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Global.Herbler
{
    public static class DateTimeExtensions
    {
        public static string FormatToString(this DateTime date)
            => date.ToString("yyyy-MM-dd");

        public static string FormatToString(this DateTime? date)
            => date == null ? string.Empty : date.GetValueOrDefault().ToString("yyyy-MM-dd");

        public static bool IsValid(this DateTime date) => date.Year <= 2100 && date.Year >= 1900;
        public static DateTime ToDateTime(this string dateTimeString)
        {
            DateTime dateTime;
            TryConvert.ToDate(dateTimeString, out dateTime);
            return dateTime;
        }
        public static DateTime? ToNullableDateTime(this string dateTimeString)
        {
            DateTime dateTime;

            if (TryConvert.ToDate(dateTimeString, out dateTime))
                return dateTime;

            return null;
        }
    }
}