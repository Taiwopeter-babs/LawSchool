using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LawSchool.Models;

[Table("students")]
public class Student
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("firstName")]
    [Required]
    public string? FirstName { get; set; }

    [Column("lastName")]
    [Required]
    public string? LastName { get; set; }

    [Column("email")]
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Column("department")]
    [Required]
    public string? Department { get; set; }

    [Column("gpa")]
    [Required]
    public decimal? GPA { get; set; }

    /// <summary>
    /// Skip navigation for student - many courses in many-many
    /// </summary>
    public List<Course>? Courses { get; } = [];
    public List<Enrollment>? Enrollments { get; } = [];
}