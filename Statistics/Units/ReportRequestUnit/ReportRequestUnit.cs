using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Statistics.Entities;
using Statistics.Helpers;
using Statistics.Options;
using static Statistics.Units.ReportRequestUnit.IReportRequestUnit;

namespace Statistics.Units.ReportRequestUnit;

public class ReportRequestUnit : IReportRequestUnit
{
    private readonly StatisticsContext _context;
    private readonly ReportOptions _options;

    public ReportRequestUnit(StatisticsContext context, IOptions<ReportOptions> options)
    {
        _context = context;
        _options = options.Value;
    }

    public async Task<CreatedReportRequestModel> CreateReportRequest(CreateReportRequestModel model)
    {
        var reportRequest = new ReportRequest
        {
            From = model.From,
            To = model.To,
            Request = new Request
            {
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Status = RequestStatus.Created
            },
            UserId = model.UserId
        };

        await _context.ReportRequests.AddAsync(reportRequest);
        await _context.SaveChangesAsync();

        return new CreatedReportRequestModel { RequestId = reportRequest.Request.Id };
    }

    public async Task<ReportInfoStatusModel> GetReportInfo(GetReportInfoModel model)
    {
        var report = await _context.ReportRequests
            .AsNoTracking()
            .Include(reportRequest => reportRequest.Request)
            .Where(reportRequest => reportRequest.Request.Id == model.RequestId)
            .Include(reportRequest => reportRequest.Report)
            .Select(reportRequest => new ReportInfoStatusModel
            {
                RequestId = reportRequest.RequestId,
                Percent = reportRequest.Request.Status == RequestStatus.Created
                    ? 0
                    : ReportHelper.GetPercent(reportRequest.Request.CreatedAt, _options.Delay),
                Result = reportRequest.ReportId == null
                    ? null
                    : new ReportInfoStatusModel.ReportInfoModel
                    {
                        CountSignIn = reportRequest.Report.CountSignIn,
                        UserId = reportRequest.UserId
                    }
            }).SingleAsync();

        return report;
    }
}