using LawSchool.Contracts;
using LawSchool.Models;
using Microsoft.EntityFrameworkCore;
using LawSchool.Utilities;

namespace LawSchool.Data;

public class StudentRepository : SchoolBase<Student>, IStudentRepository
{
    public StudentRepository(SchoolContext schoolcontext) : base(schoolcontext)
    {
    }

    public void CreateStudent(Student student) => Create(student);


    public void DeleteStudent(Student student) => Delete(student);

    public async Task<PagedList<Student>> GetAllStudentsAsync(StudentParameters studentParameters,
         bool trackChanges)
    {
        var students = await FindAll(trackChanges)
            .OrderBy(st => st.FirstName)
            .ToListAsync();

        var studentsCount = await FindAll(trackChanges).CountAsync();

        return PagedList<Student>.ToPagedList(students, studentsCount,
                studentParameters.PageNumber, studentParameters.PageSize);
    }

    public async Task<Student?> GetStudentAsync(int studentId, bool trackChanges)
    {
        return await FindByCondition(st => st.Id.Equals(studentId), trackChanges)
            .SingleOrDefaultAsync();
    }

    public async Task<Student?> GetStudentByEmailAsync(string studentEmail, bool trackChanges)
    {
        return await FindByCondition(st => st.Email.Equals(studentEmail, StringComparison.InvariantCultureIgnoreCase),
                            trackChanges)
                    .SingleOrDefaultAsync();
    }
}