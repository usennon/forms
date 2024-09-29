using IW5.Models.Entities;

namespace IW5.DAL.Contracts
{
    public interface IOptionRepository : IRepo<Option>
    {
        Task<IEnumerable<Option>> GetAllOptionsAsync(bool trackChanges);
        Task<IEnumerable<Option>> GetOptionsFromQuestionAsync(Guid id, bool trackChanges);
        Task<Option> GetOptionByIdAsync(Guid userId, bool trackChanges);
        void CreateOptionForQuestion(Guid questionId, Option option);
        void CreateListOfOptionsForQuestion(Guid questionId, List<Option> options);
        void DeleteOption(Option option);
    }
}
