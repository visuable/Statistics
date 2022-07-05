using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Statistics.Entities;
using Statistics.Options;
using Statistics.Units;
using Statistics.Units.ReportRequestUnit;
using Xunit;

namespace Statistics.Tests;

public class ReportRequestUnitTests : BaseTest
{
    static ReportRequestUnitTests()
    {
        Services.AddStatisticsContextInMemory();
        Services.AddScoped<IReportRequestUnit, ReportRequestUnit>();
        Services.AddOptions();
        Services.Configure<ReportOptions>(_ => _.Delay = 10000);
        ServiceProvider = Services.BuildServiceProvider();
    }

    [Fact]
    public static async Task ReportMustExists()
    {
        var service = ServiceProvider.GetRequiredService<IReportRequestUnit>();
        var context = ServiceProvider.GetRequiredService<StatisticsContext>();
        var user = new User.Builder()
            .HasIdentity()
            .HasContact("88005553535", "example@mail.ru")
            .Build();
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        var result = await service.CreateReportRequest(new IReportRequestUnit.CreateReportRequestModel
        {
            From = DateTime.UtcNow,
            To = DateTime.UtcNow,
            UserId = user.Id
        });
        var actual = await context.Requests.FindAsync(result.RequestId);

        Assert.NotNull(actual);
    }

    [Fact]
    public static async Task ReportInfoPercentMustBe0()
    {
        var service = ServiceProvider.GetRequiredService<IReportRequestUnit>();
        var context = ServiceProvider.GetRequiredService<StatisticsContext>();
        var user = new User.Builder()
            .HasIdentity()
            .HasContact("88005553535", "example@mail.ru")
            .Build();
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        var result = await service.CreateReportRequest(new IReportRequestUnit.CreateReportRequestModel
        {
            From = DateTime.UtcNow,
            To = DateTime.UtcNow,
            UserId = user.Id
        });
        var actual = await service.GetReportInfo(new IReportRequestUnit.GetReportInfoModel
            { RequestId = result.RequestId });

        Assert.NotNull(actual);
        Assert.Equal(0, actual.Percent);
        Assert.Equal(result.RequestId, actual.RequestId);
        Assert.Null(actual.Result);
    }
}