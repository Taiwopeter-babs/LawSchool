using LawSchool.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/errors/error-development");
}
else
{
    app.UseExceptionHandler("/errors/error");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
