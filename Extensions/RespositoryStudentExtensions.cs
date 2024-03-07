using LawSchool.Models;

namespace LawSchool.Extensions;

public static class RepositoryStudentExtensions
{
    public static IQueryable<Student> FilterStudentsByGpa(this IQueryable<Student> students,
        decimal minGpa, decimal maxGpa)
    {
        return students.Where(st => (st.GPA >= minGpa && st.GPA <= maxGpa));
    }

    public static IQueryable<Student> FilterStudentsByDepartment(this IQueryable<Student> students,
        string department)
    {
        if (department is null)
            return students;

        return students
            .Where(st => st.Department.Equals(department, StringComparison.InvariantCultureIgnoreCase));
    }
}