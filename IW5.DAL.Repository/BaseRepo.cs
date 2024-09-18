using IW5.Models.Entities;
using IW5.DAL.Contracts;
using Microsoft.EntityFrameworkCore;

namespace IW5.DAL.Repository
{
    public abstract class BaseRepo<T> : IRepo<T> where T : BaseEntity, new()
    {
        private readonly bool _disposeContext;
        public DbSet<T> Table { get; }
        public FormsDbContext Context { get; }

        protected BaseRepo(FormsDbContext context)
        {
            Context = context;
            Table = Context.Set<T>();
            _disposeContext = false;
        }

        protected BaseRepo(DbContextOptions<FormsDbContext> options) : this(new FormsDbContext(options))
        {
            _disposeContext = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _isDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (_disposeContext)
                {
                    Context.Dispose();
                }
            }

            _isDisposed = true;
        }

        ~BaseRepo()
        {
            Dispose(false);
        }

        public virtual T? Find(Guid? id) => Table.Find(id);
        public virtual T? FindAsNoTracking(Guid id)
            => Table.AsNoTrackingWithIdentityResolution().FirstOrDefault(x => x.Id == id);

        public T? FindIgnoreQueryFilters(Guid id)
            => Table.IgnoreQueryFilters().FirstOrDefault(x => x.Id == id);

        public virtual IEnumerable<T> GetAll() => Table;
        public virtual IEnumerable<T> GetAllIgnoreQueryFilters() => Table.IgnoreQueryFilters();

        public void ExecuteQuery(string sql, object[] sqlParametersObjects)
            => Context.Database.ExecuteSqlRaw(sql, sqlParametersObjects);

        public virtual int Add(T entity, bool persist = true)
        {
            Table.Add(entity);
            return persist ? SaveChanges() : 0;
        }

        public virtual int AddRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.AddRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public virtual int Update(T entity, bool persist = true)
        {
            Table.Update(entity);
            return persist ? SaveChanges() : 0;
        }

        public virtual int UpdateRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.UpdateRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public int Delete(Guid id, bool persist = true)
        {
            var entity = new T { Id = id };
            Context.Entry(entity).State = EntityState.Deleted;
            return persist ? SaveChanges() : 0;
        }

        public virtual int Delete(T entity, bool persist = true)
        {
            Table.Remove(entity);
            return persist ? SaveChanges() : 0;
        }

        public virtual int DeleteRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.RemoveRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public int SaveChanges()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Ploho", ex);
            }
        }
    }
}
