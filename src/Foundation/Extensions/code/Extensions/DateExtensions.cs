using Sitecore;
using System;
using System.Globalization;

namespace SitecoreDiser.Extensions.Extensions
{
    public static class DateExtensions
    {
        public const string DateTimeFormat = "dd/MM/yyyy HH:mm";
        public const string DateTimeDisplayFormat = "dd MMM yyyy hh:mm tt";
        public const string DateDisplayFormat = "dd MMM yyyy";

        public static string GetFormattedDateWithTimeZone(string dateTime, string timeZone, bool grantsTimeZone = false)
        {
            var date = DateUtil.ParseDateTime(dateTime, DateTime.MinValue);
            if (date != DateTime.MinValue)
            {
                date = DateUtil.ToServerTime(date);
                if (grantsTimeZone)
                {
                    timeZone = date.IsDaylightSavingTime() ? "AEDT" : "AEST";
                }
            }
            return (date == DateTime.MinValue) ? "" : date.ToString(DateTimeDisplayFormat) + (!string.IsNullOrEmpty(timeZone) ? " " + timeZone : "");
        }
        public static string GetDisplayDate(string dateTimeString)
        {
            var dateTime = DateUtil.ParseDateTime(dateTimeString, DateTime.MinValue);
            dateTime = DateUtil.ToServerTime(dateTime);
            return dateTime != DateTime.MinValue ? dateTime.Date.ToString(DateDisplayFormat) : string.Empty;
        }

        /// <summary>
        /// Converts date and time strings into a nullable date time
        /// </summary>
        public static DateTime? GetDate(string date, string time, int optionalFutureDay = 0)
        {
            return string.IsNullOrEmpty(date)
                ? (DateTime?)null
                : DateUtil.ToUniversalTime(DateTime.ParseExact(
                        string.IsNullOrEmpty(time) ? $"{date} 00:00" : $"{date} {time}",
                        DateTimeFormat, CultureInfo.CurrentCulture))
                    .AddDays(optionalFutureDay);
        }

        /// </summary>
        /// <param name="utcDatetime">UTC datetime</param>
        /// <returns></returns>
        public static DateTime? GetServerDate(DateTime? utcDatetime)
        {
            return utcDatetime == null ? (DateTime?)null : DateUtil.ToServerTime(utcDatetime.Value);
        }

        /// </summary>
        /// <param name="utcDatetime">UTC datetime</param>
        /// <returns>Formatted display datetime string</returns>
        public static string GetFormattedServerDateTime(DateTime utcDatetime)
        {
            return DateUtil.ToServerTime(utcDatetime).ToString(DateTimeDisplayFormat);
        }
    }
}