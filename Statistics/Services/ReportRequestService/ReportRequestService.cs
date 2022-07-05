using Microsoft.EntityFrameworkCore;
using Statistics.Entities;
using Statistics.Units;
using static Statistics.Services.ReportRequestService.IReportRequestService;

namespace Statistics.Services.ReportRequestService;

public class ReportRequestService : IReportRequestService
{
    private readonly StatisticsContext _context;

    public ReportRequestService(StatisticsContext context)
    {
        _context = context;
    }

    public async Task UpdateStatus(Guid reportRequestId, RequestStatus status)
    {
        var reportRequest = await _context.ReportRequests.Where(reportRequest => reportRequest.Id == reportRequestId)
            .Include(reportRequest => reportRequest.Request)
            .Select(reportRequest => reportRequest.Request)
            .SingleAsync();
        reportRequest.Status = status;

        _context.Requests.Update(reportRequest);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<UnprocessedReportRequestModel>> GetUnprocessed()
    {
        var unprocessedRequests = await _context.ReportRequests
            .AsNoTracking()
            .Include(reportRequest => reportRequest.Report)
            .Include(reportRequest => reportRequest.Request)
            .Where(reportRequest => reportRequest.Request.Status == RequestStatus.Created)
            .Select(reportRequest => new UnprocessedReportRequestModel
            {
                Id = reportRequest.Id,
                From = reportRequest.From,
                To = reportRequest.To,
                UserId = reportRequest.UserId
            })
            .ToListAsync();

        return unprocessedRequests;
    }

    public async Task SetReport(Guid reportRequestId, Guid reportId)
    {
        var reportRequest = await _context.ReportRequests.FindAsync(reportRequestId);
        reportRequest!.ReportId = reportId;
        _context.ReportRequests.Update(reportRequest);
        await _context.SaveChangesAsync();
    }
}