using AutoMapper;
using LawSchool.Contracts;
using LawSchool.Exceptions;
using LawSchool.Models;
using LawSchool.ModelsDto;
using LawSchool.Utilities;

namespace LawSchool.Services;

internal sealed class StudentService : IStudentService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public StudentService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<StudentDto> CreateStudentAsync(StudentForCreationDto studentDto, bool trackchanges)
    {
        var student = await _repository.Student.GetStudentByEmailAsync(studentDto.Email, trackchanges);
        if (student != null)
            throw new StudentAlreadyExistsException(studentDto.Email);

        var studentEntity = _mapper.Map<Student>(studentDto);

        _repository.Student.CreateStudent(studentEntity);

        await _repository.SaveAsync();

        return _mapper.Map<StudentDto>(studentEntity);
    }

    public async Task<(IEnumerable<StudentDto> students, PageMetaData pageMetaData)>
        GetAllStudentsAsync(StudentParameters studentParameters, bool trackChanges)
    {
        var studentsWithPageMetadata = await _repository.Student
                    .GetAllStudentsAsync(studentParameters, trackChanges);

        var studentsDto = _mapper.Map<IEnumerable<StudentDto>>(studentsWithPageMetadata);

        return (students, pageMetaData: studentsWithPageMetadata.PageMetaData);
    }

    public async Task<StudentDto> GetStudentAsync(int id, bool trackChanges)
    {
        var student = await _repository.Student.GetStudentAsync(id, trackChanges) ??
            throw new StudentNotFoundException(id);

        return _mapper.Map<StudentDto>(student);
    }

    public async Task DeleteStudentAsync(int id, bool trackChanges)
    {
        var studentEntity = await _repository.Student.GetStudentAsync(id, trackChanges) ??
            throw new StudentNotFoundException(id);
        _repository.Student.DeleteStudent(studentEntity);

        await _repository.SaveAsync();
    }
}