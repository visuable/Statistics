using System;
using Statistics.Helpers;
using Xunit;

namespace Statistics.Tests;

public static class ReportHelperTests
{
    [Fact]
    public static void GetPercentMustBe100()
    {
        var createdAt = DateTime.Now.AddDays(-1);
        var delay = 1000;

        var percent = ReportHelper.GetPercent(createdAt, delay);

        Assert.Equal(100, percent);
    }

    [Fact]
    public static void GetPercentMustBeAround50()
    {
        var createdAt = DateTime.UtcNow.AddMinutes(-15);
        var delay = 1800000;

        var percent = ReportHelper.GetPercent(createdAt, delay);

        Assert.Equal(50, (int)percent);
    }
}