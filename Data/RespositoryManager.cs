using LawSchool.Contracts;

namespace LawSchool.Data;

public sealed class RepositoryManager : IRepositoryManager
{

    private readonly SchoolContext _schoolContext;
    private readonly Lazy<StudentRepository> _studentRepository;

    public RepositoryManager(SchoolContext schoolContext)
    {
        _schoolContext = schoolContext;
        _studentRepository = new Lazy<StudentRepository>(() =>
            new StudentRepository(_schoolContext));
    }

    public IStudentRepository Student => _studentRepository.Value;

    public async Task SaveAsync() => await _schoolContext.SaveChangesAsync();
}