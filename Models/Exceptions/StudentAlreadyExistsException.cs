namespace LawSchool.Exceptions;

public sealed class StudentAlreadyExistsException : BadRequestException
{
    public StudentAlreadyExistsException(string studentEmail) :
        base($"The student with the email: {studentEmail}, already exists")
    { }
}