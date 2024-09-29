using IW5.Models.Entities;
using System.Linq.Expressions;

namespace IW5.DAL.Contracts
{
    public interface IRepo<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAll(bool trackChanges);
        Task<T> GetById(Guid id, bool trackChanges);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
