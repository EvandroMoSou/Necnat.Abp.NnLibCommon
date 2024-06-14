using System;

namespace Necnat.Abp.NnLibCommon.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime FirstDayOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        public static DateTime FirstDayOfYear(this DateTime dt)
        {
            return new DateTime(dt.Year, 1, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month), 23, 59, 59, 999);
        }

        public static DateTime LastDayOfYear(this DateTime dt)
        {
            return new DateTime(dt.Year, 12, DateTime.DaysInMonth(dt.Year, dt.Month), 23, 59, 59, 999);
        }
    }
}
