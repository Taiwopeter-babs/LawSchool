using Microsoft.EntityFrameworkCore;
using LawSchool.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace LawSchool.Data;

public class SchoolContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }


    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {
        try
        {
            var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;

            if (databaseCreator != null)
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
            Console.WriteLine(ex.Message);
        }
    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // modelBuilder.Entity<Student>()
        // .HasMany(st => st.Courses)
        // .WithMany(c => c.Students)
        // .UsingEntity(
        //     l => l.HasOne(typeof(Course)).WithMany().HasForeignKey("CourseForeignKey"),
        //     r => r.HasOne(typeof(Student)).WithMany().HasForeignKey("StudentForeignKey")
        // );
        modelBuilder.Entity<Student>()
        .HasMany(st => st.Courses)
        .WithMany(c => c.Students)
        .UsingEntity<Enrollment>();
    }
}