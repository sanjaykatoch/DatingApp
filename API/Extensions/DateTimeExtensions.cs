using System;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime dob){
            var todayDate=DateTime.Today;
            var age=todayDate.Year-dob.Year;
            if(todayDate.Date>todayDate.AddYears(-age)) return age--;
            return age;
        }
    }
}