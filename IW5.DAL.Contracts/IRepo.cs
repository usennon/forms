using IW5.Models.Entities;
using System.Linq.Expressions;

namespace IW5.DAL.Contracts
{
    public interface IRepo<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAll(bool trackChanges);
        Task<T> GetByIdAsync(Guid id, bool trackChanges);
        void Create(T entity);
        Task UpdateAsync(T entity);
        void Delete(T entity);
    }
}
