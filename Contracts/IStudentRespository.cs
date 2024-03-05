using LawSchool.Models;
using LawSchool.Utilities;

namespace LawSchool.Contracts;

public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetAllStudentsAsync(StudentParameters studentParameters,
        bool trackChanges);
    Task<Student?> GetStudentAsync(int studentId, bool trackChanges);
    Task<Student?> GetStudentByEmailAsync(string studentEmail, bool trackChanges);
    void CreateStudent(Student student);
    void DeleteStudent(Student student);
}