namespace LawSchool.Contracts;

public interface IServiceManager
{
    IStudentService StudentService { get; }
}