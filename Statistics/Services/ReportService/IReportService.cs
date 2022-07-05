namespace Statistics.Services.ReportService
{
    public interface IReportService
    {
        Task<Guid> GenerateReport(DateTime from, DateTime to, Guid userId);
    }
}
