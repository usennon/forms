using IW5.Models.Entities;
using IW5.DAL.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IW5.DAL.Repository
{
    public abstract class BaseRepo<T>(FormsDbContext context) : IRepo<T> where T : BaseEntity, new()
    {
        protected FormsDbContext context = context;
        protected readonly DbSet<T> _dbSet = context.Set<T>();
        protected virtual ICollection<string> NavigationPathDetail => new List<string>();

        public IQueryable<T> GetAll(bool trackChanges) => !trackChanges ?
            _dbSet.AsNoTracking() : _dbSet;

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges,
            params Expression<Func<T, object>>[] includes)
        {
            var query = !trackChanges ?
            _dbSet.Where(expression).AsNoTracking() : _dbSet.Where(expression);

            if (includes != null && includes.Any())
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }

        public async ValueTask<bool> ExistsAsync(T entity) => entity.Id != Guid.Empty
                && await _dbSet.AnyAsync(e => e.Id == entity.Id).ConfigureAwait(false);
        public void Create(T entity) => _dbSet.Add(entity);

        public void Update(T entity)
        {
            if (ExistsAsync(entity).Result)
            {
                _dbSet.Attach(entity);
                _dbSet.Update(entity);
            }
        }

        public void Delete(T entity) => _dbSet.Remove(entity);
       
    }
}
