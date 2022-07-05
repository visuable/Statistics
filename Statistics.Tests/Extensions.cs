using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Statistics.Units;

namespace Statistics.Tests
{
    public static class Extensions
    {
        public static IServiceCollection AddStatisticsContextInMemory(this IServiceCollection services)
        {
            services.AddDbContext<StatisticsContext>(options => options.UseInMemoryDatabase("statistics_memory"));
            return services;
        }
    }
}
