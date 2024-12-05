using IW5.Models.Entities;
using IW5.DAL.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AutoMapper;

namespace IW5.DAL.Repository
{
    public abstract class BaseRepo<T>(FormsDbContext context) : IRepo<T> where T : BaseEntity, new()
    {
        private readonly FormsDbContext context = context;
        private readonly DbSet<T> _dbSet = context.Set<T>();


        protected IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges,
            params Expression<Func<T, object>>[]? includes)
        {
            var query = !trackChanges ?
            _dbSet.Where(expression).AsNoTracking() : _dbSet.Where(expression);

            if (includes != null && includes.Any())
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }
        public async ValueTask<bool> ExistsAsync(Guid id)
            => id != Guid.Empty
            && await _dbSet.AnyAsync(e => e.Id == id).ConfigureAwait(false);
        public virtual IQueryable<T> GetAll(bool trackChanges) => !trackChanges ?
            _dbSet.AsNoTracking() : _dbSet;
        public virtual async Task<T> GetByIdAsync(Guid id, bool trackChanges)
            => await GetByCondition(e => e.Id == id, trackChanges).SingleOrDefaultAsync();

        public virtual void Create(T entity) => _dbSet.Add(entity);

        public virtual void CreateRange(IEnumerable<T> entities) => _dbSet.AddRange(entities);

        public async Task UpdateAsync(T entity)
        {
            if (await ExistsAsync(entity.Id))
            {
                _dbSet.Update(entity!);
            }
        }


        public virtual void Delete(T entity) => _dbSet.Remove(entity);

    }
}
