namespace LawSchool.Utilities;

public class StudentParameters : RequestParameters
{
    public decimal MinGpa { get; set; } = 0.00M;
    public decimal MaxGpa { get; set; } = 5.00M;
    public string? Department { get; set; }

    public bool ValidGpaRange => MaxGpa > MinGpa;
}