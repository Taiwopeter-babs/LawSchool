using System.ComponentModel.DataAnnotations;


namespace LawSchool.Models;
public class StudentDTO
{
    public int Id { get; set; }

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? Department { get; set; }

    [Required]
    public decimal? GPA { get; set; }
}