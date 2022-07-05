using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Statistics.Entities;

namespace Statistics.Units;

public class StatisticsContext : DbContext
{
    public StatisticsContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Session> Sessions { get; set; }
    public DbSet<ReportRequest> ReportRequests { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Report> Reports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}