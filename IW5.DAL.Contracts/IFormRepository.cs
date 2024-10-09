using IW5.Models.Entities;

namespace IW5.DAL.Contracts
{
    public interface IFormRepository : IRepo<Form>
    {
        Task<Form> GetFormByTitleAsync(string title, bool trackChanges);
        Task<IEnumerable<Form>?> GetFormByCreatedAtAsync(bool trackChanges, DateTime? start, DateTime? end);
        Task<IEnumerable<Form>?> GetActiveFormsAsync(bool trackChanges);
        IQueryable<Form> SearchFormByTitle(string title, bool trackChanges);

    }
}
