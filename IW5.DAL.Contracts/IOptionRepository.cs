using IW5.Models.Entities;

namespace IW5.DAL.Contracts
{
    public interface IOptionRepository : IRepo<Option>
    {
        Task<IEnumerable<Option>> GetOptionsFromQuestionAsync(Guid id, bool trackChanges);

    }
}
