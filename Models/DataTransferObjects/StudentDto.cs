
namespace LawSchool.ModelsDto;

public record StudentDto
{
   public int Id { get; init; }
   public string? FullName { get; init; }
   public string? Email { get; init; }
   public string? Department { get; init; }
   public decimal GPA { get; init; }
}