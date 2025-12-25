namespace School.Services
{
    public static class PersianDateHelper
    {
        public static string ToPersianDate(this DateTime date)
        {
            var pc = new System.Globalization.PersianCalendar();
            return $"{pc.GetYear(date)}/{pc.GetMonth(date):D2}/{pc.GetDayOfMonth(date):D2}";
        }
    }
}