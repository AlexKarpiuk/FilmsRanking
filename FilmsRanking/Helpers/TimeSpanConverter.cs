namespace FilmsRanking.Helpers
{
    public static class TimeSpanConverter
    {
        public static TimeSpan ConvertMinutesToTimeSpan(int minutes)
        {
            return TimeSpan.FromMinutes(minutes);
        }
    }
}
