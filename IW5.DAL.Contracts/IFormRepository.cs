using IW5.Models.Entities;

namespace IW5.DAL.Contracts
{
    public interface IFormRepository : IRepo<Form>
    {
        Task<IEnumerable<Form>?> GetFormByTitleAsync(string title, bool trackChanges);
        Task<IEnumerable<Form>?> GetFormByCreatedAt(bool trackChanges, DateTime? start, DateTime? end);
        Task<IEnumerable<Form>?> GetActiveForms(bool trackChanges);
    }
}
