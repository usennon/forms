namespace IW5.API.DAL.Repositories.Interfaces
{
    public interface IRepo<T> : IDisposable
    {
        int Add(T entity, bool persist = true);
        int AddRange(IEnumerable<T> entities, bool persist = true);
        int Update(T entity, bool persist = true);
        int UpdateRange(IEnumerable<T> entities, bool persist = true);
        int Delete(Guid id, bool persist = true);
        int Delete(T entity, bool persist = true);
        int DeleteRange(IEnumerable<T> entities, bool persist = true);
        T? Find(Guid? id);
        T? FindAsNoTracking(Guid id);
        T? FindIgnoreQueryFilters(Guid id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllIgnoreQueryFilters();
        void ExecuteQuery(string sql, object[] sqlParametersObjects);
        int SaveChanges();
    }
}
