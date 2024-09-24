using IW5.Models.Entities;

namespace IW5.DAL.Contracts
{
    public interface IFormRepository : IRepo<Form>
    {
        Task<IEnumerable<Form>> GetAllFormsAsync(bool trackChanges);
        Task<Form> GetFormById(Guid Id, bool trackChanges);

    }
}
