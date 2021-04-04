using System;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int calculateAge(this DateTime dob)
        {
            var todayDate = DateTime.Today;
            int age=todayDate.Year-dob.Year;
            if(dob.Date>todayDate.AddYears(-age)) age--;
            return age;
        }
    }
}