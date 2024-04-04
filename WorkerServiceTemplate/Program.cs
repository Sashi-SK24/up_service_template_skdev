using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using WorkerServiceTemplate;
using WorkerServiceTemplate.Repositories;
using WorkerServiceTemplate.Repositories.IRepositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using WorkerServiceTemplate.Profiles;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Set logger
Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
    .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + "/Logs/Debug-.log", rollingInterval: RollingInterval.Day)
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

builder.WebHost.UseKestrel((context, serverOptions) =>
{
    serverOptions.Listen(IPAddress.Loopback, 5001);
    serverOptions.ListenAnyIP(5001);
});

// Set DB Context
var connectionString = "Server=.\\ENTRYPASS;Database=SampleDB;User Id=sa;Password=admin123;encrypt=false";
builder.Services.AddDbContext<WorkerServiceDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();
builder.Services.AddAutoMapper(typeof(WeatherProfiles));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddWindowsService();
builder.Services.AddHostedService<BackgroundWorkerService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Set Fluent Validation
var assembly = typeof(Program).Assembly;
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Run DB Migration
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WorkerServiceDbContext>();
    db.Database.Migrate();
}

app.UseStaticFiles();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
