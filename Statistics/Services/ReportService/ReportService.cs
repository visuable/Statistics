using Microsoft.EntityFrameworkCore;

using Statistics.Entities;
using Statistics.Units;

namespace Statistics.Services.ReportService
{
    public class ReportService : IReportService
    {
        private readonly StatisticsContext _context;

        public ReportService(StatisticsContext context)
        {
            _context = context;
        }

        public async Task<Guid> GenerateReport(DateTime from, DateTime to, Guid userId)
        {
            var report = new Report()
            {
                CountSignIn = await _context.Sessions.AsNoTracking()
                                                     .Where(session => session.UserId == userId)
                                                     .CountAsync(session => session.CreatedAt >= from && session.CreatedAt < to),
                UserId = userId
            };
            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
            return report.Id;
        }
    }
}
