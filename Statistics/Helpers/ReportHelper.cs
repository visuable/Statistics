namespace Statistics.Helpers
{
    public static class ReportHelper
    {
        public static decimal GetPercent(DateTime createdAt, int delay)
        {
            var now = DateTime.UtcNow;

            var elapsed = now - createdAt;
            var percent = elapsed / TimeSpan.FromMilliseconds(delay);
            if (percent >= 1) return 100m;
            return (decimal) percent * 100m;
        }
    }
}
