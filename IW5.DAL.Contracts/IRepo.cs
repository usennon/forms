using System.Linq.Expressions;

namespace IW5.DAL.Contracts
{
    public interface IRepo<T> : IDisposable
    {
        IQueryable<T> GetAll(bool trackChanges);
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
