using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using ArgaamSchedular.Entities;
using ArgaamSchedular.Services;
using ArgaamSchedular.Services.Implementations;
using ArgaamSchedular.Services.Interface;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("HangfireConnection");

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ArgaamPlusContext>(options => options.UseSqlServer(connString));

//adding service
builder.Services.AddTransient<IRecoringService, RecoringService>();

builder.Services.AddTransient<IJobTestService, JobTestService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<>



// Add Hangfire services.
builder.Services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                UsePageLocksOnDequeue = true,
                DisableGlobalLocks = true
            }));

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseHangfireDashboard();

app.Run();
