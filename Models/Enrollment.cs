using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LawSchool.Models;

[Table("students_courses")]
public class Enrollment
{

    [Column("studentId")]
    public int StudentId { get; set; }

    [Column("courseId")]
    public int CourseId { get; set; }

    [Column("dateEnrolled")]
    public DateTime DateEnrolled { get; set; } = DateTime.Now;

    public Student Student { get; set; } = null!;
    public Course Course { get; set; } = null!;

}