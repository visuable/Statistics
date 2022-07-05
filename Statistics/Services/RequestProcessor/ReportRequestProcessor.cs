using Microsoft.Extensions.Options;
using Statistics.Entities;
using Statistics.Options;
using Statistics.Services.ReportRequestService;
using Statistics.Services.ReportService;

namespace Statistics.Services.RequestProcessor;

public class ReportRequestProcessor : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ReportRequestProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var options = scope.ServiceProvider.GetRequiredService<IOptions<ReportOptions>>().Value;

        while (!stoppingToken.IsCancellationRequested)
        {
            var reportRequestService = scope.ServiceProvider.GetRequiredService<IReportRequestService>();
            var reportService = scope.ServiceProvider.GetRequiredService<IReportService>();

            var unprocessedRequests = await reportRequestService.GetUnprocessed();

            if (!unprocessedRequests.Any())
            {
                await Task.Delay(1000);
                continue;
            }

            foreach (var unprocessedRequest in unprocessedRequests)
            {
                await reportRequestService.UpdateStatus(unprocessedRequest.Id, RequestStatus.Processing);

                var reportId = await reportService.GenerateReport(unprocessedRequest.From, unprocessedRequest.To,
                    unprocessedRequest.UserId);

                await Task.Delay(options.Delay, stoppingToken);
                await reportRequestService.SetReport(unprocessedRequest.Id, reportId);
                await reportRequestService.UpdateStatus(unprocessedRequest.Id, RequestStatus.Ready);
            }
        }
    }
}