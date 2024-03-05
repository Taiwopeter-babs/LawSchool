namespace LawSchool.Contracts;

public interface IRepositoryManager
{
    IStudentRepository Student { get; }
    Task SaveAsync();
}