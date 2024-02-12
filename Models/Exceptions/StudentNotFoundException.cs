namespace LawSchool.Exceptions;

public sealed class StudentNotFoundException : NotFoundException
{
    public StudentNotFoundException(int studentId) :
        base($"The student with the id: {studentId}, was not found in the database")
    { }
}