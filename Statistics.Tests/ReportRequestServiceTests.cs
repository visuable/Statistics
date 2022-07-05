using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Statistics.Entities;
using Statistics.Services.ReportRequestService;
using Statistics.Units;
using Xunit;

namespace Statistics.Tests
{
    public class ReportRequestServiceTests : BaseTest
    {
        static ReportRequestServiceTests()
        {
            Services.AddStatisticsContextInMemory();
            Services.AddScoped<IReportRequestService, ReportRequestService>();
            ServiceProvider = Services.BuildServiceProvider();
        }

        [Fact]
        public static async Task StatusMustBeProcessing()
        {
            await StatusMustBe(RequestStatus.Processing);
        }

        [Fact]
        public static async Task StatusMustBeReady()
        {
            await StatusMustBe(RequestStatus.Ready);
        }

        public static async Task StatusMustBe(RequestStatus expectedStatus)
        {
            var context = ServiceProvider.GetRequiredService<StatisticsContext>();
            var service = ServiceProvider.GetRequiredService<IReportRequestService>();
            var user = new User.Builder()
                .HasIdentity()
                .HasContact("88005553535", "example@mail.ru")
                .Build();

            await context.Users.AddAsync(user);
            var reportRequest = new ReportRequest.Builder()
                .WithRequest()
                .WithRange(DateTime.UtcNow, DateTime.UtcNow)
                .ToUser(user.Id)
                .Build();
            await context.ReportRequests.AddAsync(reportRequest);
            await context.SaveChangesAsync();

            await service.UpdateStatus(reportRequest.Id, expectedStatus);
            var actual = await context.Requests.FirstAsync(request => request.Id == reportRequest.RequestId);

            Assert.Equal(expectedStatus, actual.Status);
        }
    }
}
