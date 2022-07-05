using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Statistics.Entities;
using Statistics.Services.ReportService;
using Statistics.Units;
using Xunit;

namespace Statistics.Tests
{
    public class ReportServiceTests : BaseTest
    {
        static ReportServiceTests()
        {
            Services.AddStatisticsContextInMemory();
            Services.AddScoped<IReportService, ReportService>();
            ServiceProvider = Services.BuildServiceProvider();
        }

        [Fact]
        public static async Task ReportMustBeEmpty()
        {
            var context = ServiceProvider.GetRequiredService<StatisticsContext>();
            var service = ServiceProvider.GetRequiredService<IReportService>();
            var user = new User.Builder()
                .HasIdentity()
                .HasContact("88005553535", "example@mail.ru")
                .Build();
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            var id = await service.GenerateReport(DateTime.UtcNow, DateTime.UtcNow, user.Id);
            var actual = await context.Reports.FindAsync(id);

            Assert.NotNull(actual);
            Assert.Equal(0, actual.CountSignIn);
        }

        [Fact]
        public static async Task ReportMustBeInRange()
        {
            var context = ServiceProvider.GetRequiredService<StatisticsContext>();
            var service = ServiceProvider.GetRequiredService<IReportService>();
            var user = new User.Builder()
                .HasIdentity()
                .HasContact("88005553535", "example@mail.ru")
                .Build();
            user.Sessions = new List<Session>()
            {
                new Session.Builder().Build(),
                new Session.Builder().Build(),
                new Session.Builder().Build(),
                new Session.Builder().Build(),
                new Session.Builder().Build(),
                new Session.Builder().Build(),
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            var id = await service.GenerateReport(DateTime.UtcNow.AddMinutes(-1), DateTime.UtcNow.AddMinutes(1), user.Id);
            var actual = await context.Reports.FindAsync(id);

            Assert.NotNull(actual);
            Assert.Equal(6, actual.CountSignIn);
        }
    }
}
