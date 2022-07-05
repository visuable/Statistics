using Microsoft.EntityFrameworkCore;
using Statistics.Options;
using Statistics.Services.ReportRequestService;
using Statistics.Services.ReportService;
using Statistics.Services.RequestProcessor;
using Statistics.Units;
using Statistics.Units.ReportRequestUnit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddHostedService<ReportRequestProcessor>()
    .AddScoped<IReportRequestUnit, ReportRequestUnit>()
    .AddScoped<IReportRequestService, ReportRequestService>()
    .AddScoped<IReportService, ReportService>()
    .AddOptions();
builder.Services.AddDbContext<StatisticsContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.Configure<ReportOptions>(builder.Configuration.GetSection(nameof(ReportOptions)).Bind);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();