
using System.ComponentModel.DataAnnotations;

namespace LawSchool.ModelsDto;

public record StudentForUpdateDto
(
    string? FirstName, string? LastName
);
