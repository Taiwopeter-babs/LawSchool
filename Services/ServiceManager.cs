using AutoMapper;
using LawSchool.Contracts;
using LawSchool.Data;

namespace LawSchool.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IStudentService> _studentService;

    public ServiceManager(IRepositoryManager repository, IMapper mapper)
    {
        _studentService = new Lazy<IStudentService>(() =>
            new StudentService(repository, mapper));
    }

    public IStudentService StudentService => _studentService.Value;
}