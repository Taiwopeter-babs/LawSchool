using LawSchool.Models;
using LawSchool.ModelsDto;
using LawSchool.Utilities;

namespace LawSchool.Contracts;

public interface IStudentService
{
    Task<StudentDto> GetStudentAsync(int id, bool trackChanges);

    Task<(IEnumerable<StudentDto> students, PageMetaData pageMetaData)>
        GetAllStudentsAsync(StudentParameters studentParameters, bool trackChanges);

    Task<StudentDto> CreateStudentAsync(StudentForCreationDto studentDTO, bool trackChanges);

    Task<(StudentForUpdateDto studentToPatch, Student studentEntity)> GetStudentForPatch(
        int id, bool trackChanges
    );

    Task SaveChangesForPatchAsync(StudentForUpdateDto studentToPatch, Student studentEntity);

    Task DeleteStudentAsync(int id, bool trackChanges);
}