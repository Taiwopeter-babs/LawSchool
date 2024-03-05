using System.Linq.Expressions;


namespace Contracts
{
    public interface ISchoolBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        void Create(T entity);
        void Delete(T entity);
        // void Update(T entity);
    }
}