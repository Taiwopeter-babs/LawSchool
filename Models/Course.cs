using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LawSchool.Models;

[Table("courses")]
public class Course
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string? CourseName { get; set; }

    [Column("weight")]
    public int? Weight { get; set; }

    /// <summary>
    /// Skip navigation for course - many students in many-many
    /// </summary>
    public List<Student> Students { get; } = [];
    public List<Enrollment> Enrollments { get; } = [];
}