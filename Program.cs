using LawSchool.Contracts;
using LawSchool.Data;
using LawSchool.Extensions;
using LawSchool.Utilities.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Configure log manager
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(),
    "/nlog.config"));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureLoggingService();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureRepositoryManager();

// suppress ApiController attribute errors to allow custom error messages
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// add action filters validation
builder.Services.AddScoped<ValidationFilter>();

// Add database dependency injection
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_ROOT_PASSWORD");


var connectionString = $"server={dbHost};port=3306;database={dbName};user=root;password={dbPassword}";

builder.Services.AddDbContext<SchoolContext>(opt => opt.UseMySQL(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
