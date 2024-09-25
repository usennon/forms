using IW5.Models.Entities;

namespace IW5.DAL.Contracts
{
    public interface IQuestionRepository : IRepo<Question>
    {
        Task<IEnumerable<Question>> GetAllQuestionsAsync(bool trackChanges);
        Task<Question> GetQuestionByIdAsync(Guid Id, bool trackChanges);
        void CreateQuestionForForm(Guid authorId, Question question);
        void DeleteQuestion(Question question);
    }
}
