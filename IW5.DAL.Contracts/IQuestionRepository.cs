using IW5.Models.Entities;

namespace IW5.DAL.Contracts
{
    public interface IQuestionRepository : IRepo<Question>
    {
        Task<IEnumerable<Question>> GetAllQuestionsFromFormAsync(bool trackChanges, Guid formId);
        Task<Question> GetQuestionByIdAsync(Guid id, bool trackChanges);
        IQueryable<Question> SearchQuestionByText(string text, bool trackChanges);
    }
}
