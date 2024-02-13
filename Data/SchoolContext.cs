using Microsoft.EntityFrameworkCore;
using LawSchool.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using LawSchool.Contracts;

namespace LawSchool.Data;

public class SchoolContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    private ILoggerManager _logger;


    public SchoolContext(DbContextOptions<SchoolContext> options, ILoggerManager logger) : base(options)
    {
        _logger = logger;
        try
        {
            if (Database.GetService<IDatabaseCreator>() is RelationalDatabaseCreator databaseCreator)
            {
                // create database if connection is not set
                if (!databaseCreator.CanConnect())
                {
                    databaseCreator.Create();
                }

                // create tables
                if (!databaseCreator.HasTables())
                {
                    databaseCreator.CreateTables();
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
        .HasMany(st => st.Courses)
        .WithMany(c => c.Students)
        .UsingEntity<Enrollment>();
    }
}