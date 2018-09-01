using System;
using System.Collections.Generic;
using System.Text;

namespace RKstat
{
    
    class ParseDate
    {
        Dictionary<string, string> MonthToNumber = new Dictionary<string, string>();

        public ParseDate()
        {
            SetMonth();
        }
        private void SetMonth()
        {
            MonthToNumber.Add("stycznia", "01");
            MonthToNumber.Add("lutego", "02");
            MonthToNumber.Add("marca", "03");
            MonthToNumber.Add("kwietnia", "04");
            MonthToNumber.Add("maja", "05");
            MonthToNumber.Add("czerwca", "06");
            MonthToNumber.Add("lipca", "07");
            MonthToNumber.Add("sierpnia", "08");
            MonthToNumber.Add("września", "09");
            MonthToNumber.Add("października", "10");
            MonthToNumber.Add("listopada", "11");
            MonthToNumber.Add("grudnia", "12");
        }

        public string GetDate(string day,string month,string year)
        {
            if (day == "" || month == "" || year == "")
                return "01-01-2000";
            string date;
            date = day + "-";
            date +=  MonthToNumber[month] + "-";
            date += year;
            return date;
        }
    }
}
