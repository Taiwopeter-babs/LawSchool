using LawSchool.ModelsDto;
using LawSchool.Utilities;

namespace LawSchool.Contracts;

public interface IStudentService
{
    Task<StudentDto> GetStudentAsync(int id, bool trackChanges);
    Task<IEnumerable<StudentDto>> GetAllStudentsAsync(StudentParameters studentParameters,
            bool trackChanges);
    Task<StudentDto> CreateStudentAsync(StudentForCreationDto studentDTO, bool trackChanges);

    Task DeleteStudentAsync(int id, bool trackChanges);
}