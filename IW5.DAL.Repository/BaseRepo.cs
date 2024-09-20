using IW5.Models.Entities;
using IW5.DAL.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IW5.DAL.Repository
{
    public abstract class BaseRepo<T>(FormsDbContext context) : IRepo<T> where T : BaseEntity, new()
    {

        private readonly DbSet<T> _dbSet = context.Set<T>();

        public IQueryable<T> GetAll(bool trackChanges) => !trackChanges ?
            _dbSet.AsNoTracking() : _dbSet;

//      public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges) => !trackChanges ?
//          _dbSet.Where(expression).AsNoTracking() : _dbSet.Where(expression);
//
        public void Create(T entity) => _dbSet.Add(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);
       
    }
}
