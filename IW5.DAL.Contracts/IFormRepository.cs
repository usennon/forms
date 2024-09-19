using IW5.Models.Entities;

namespace IW5.DAL.Contracts
{
    public interface IFormRepository
    {
        Task<IEnumerable<Form>> GetAllFormsAsync(bool trackChanges);

        Task<Form> GetByTitleAsync(string title, Guid id);
    }
}
