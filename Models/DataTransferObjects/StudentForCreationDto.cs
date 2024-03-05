
using System.ComponentModel.DataAnnotations;

namespace LawSchool.ModelsDto;

public record StudentForCreationDto
{
    [Required(ErrorMessage = "First name of student is required")]
    [MaxLength(25, ErrorMessage = "Maximum length for first name is 25 characters")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Last name of student is required")]
    [MaxLength(25, ErrorMessage = "Maximum length for last name is 25 characters")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Email address of student is required")]
    [EmailAddress]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Department of student is required")]
    public string? Department { get; set; }

    [Required(ErrorMessage = "GPA of student is required")]
    public decimal GPA { get; set; }
}
