using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public class DateTimeProcessor
    {
        public static DateTime GetCorrectedDate(DateTime dateTime)
        {
            TimeZone curTimeZone = TimeZone.CurrentTimeZone;
            TimeSpan currentOffset = curTimeZone.GetUtcOffset(DateTime.Now);
            return dateTime.AddHours(currentOffset.TotalHours);
        }

        ////ToDo
        //public static void ConvertDatesToMonth(int currentMonth, int currentYear, int daysInMonth)
        //{
        //    string StartDate;
        //    string ToDate;

        //    DateTime tempToDate = Convert.ToDateTime(currentYear.ToString() + "-" + currentMonth.ToString() + "-" + daysInMonth.ToString()).ToLocalTime();
        //    DateTime tempStartDate = Convert.ToDateTime(currentYear.ToString() + "-" + currentMonth.ToString() + "-" + "01").ToLocalTime();
        //    string toDate = tempToDate.ToString("yyyy-MM-dd-HH:mm");
        //    string startDate = tempStartDate.ToString("yyyy-MM-dd-HH:mm");
        //    StartDate = startDate;
        //    ToDate = toDate;
        //}

        ////ToDo
        //public static  void ConvertDatesToWeek()
        //{
        //    var dateFormat = CultureInfo.InvariantCulture.DateTimeFormat;
        //    int currentDay = (int)dTPFrom.Value.DayOfWeek;
        //    string tempDate = dTPFrom.Value.AddDays(-currentDay + 1).ToString("yyyy-MM-dd HH:mm");
        //    DateTime tempStartDate = Convert.ToDateTime(tempDate).ToLocalTime();
        //    string toDate = tempStartDate.AddDays(6).ToString("yyyy-MM-dd HH:mm");
        //    string startDate = tempStartDate.ToString("yyyy-MM-dd HH:mm");

        //    //Convert Dates to format to Display
        //    string toDatelabel = tempStartDate.AddDays(6).ToString("dddd, MMMM dd, yyyy");
        //    string startDatelabel = tempStartDate.ToString("dddd, MMMM dd, yyyy" + "  -  ") + tempStartDate.AddDays(4).ToString("dddd, MMMM dd, yyyy");
        //    lblApptDates.Visible = true;
        //    lblApptDates.Text = startDatelabel;
        //    StartDate = startDate;
        //    ToDate = toDate;
        //}

    }
}
