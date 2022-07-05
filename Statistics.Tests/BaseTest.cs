using System;
using Microsoft.Extensions.DependencyInjection;

namespace Statistics.Tests;

// Важно! Все тесты необходимо запускать отдельно, поскольку возникают траблы с контекстом.
public class BaseTest
{
    static BaseTest()
    {
        Services = new ServiceCollection();
    }

    public static IServiceProvider ServiceProvider { get; set; }

    protected static IServiceCollection Services { get; set; }
}