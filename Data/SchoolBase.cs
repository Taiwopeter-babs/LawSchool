using Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LawSchool.Data;

public class SchoolBase<T> : ISchoolBase<T> where T : class
{
    protected SchoolContext _schoolContext;

    public SchoolBase(SchoolContext schoolcontext) =>
    _schoolContext = schoolcontext;


    public void Create(T entity) => _schoolContext.Set<T>().Add(entity);

    public void Delete(T entity) => _schoolContext.Set<T>().Remove(entity);

    public IQueryable<T> FindAll(bool trackChanges) =>
        trackChanges ?
            _schoolContext.Set<T>().AsNoTracking() :
            _schoolContext.Set<T>();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
        trackChanges ?
             _schoolContext.Set<T>()
                .Where(expression)
                .AsNoTracking() :
             _schoolContext.Set<T>()
                .Where(expression);

    // public void Update(T entity)
    // {
    //     throw new NotImplementedException();
    // }
}