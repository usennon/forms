using IW5.BL.Models;
using IW5.Models.Entities;
using IW5.Common.Enums.Sorts;

namespace IW5.BL.API.Contracts
{
    public interface IQuestionBLogic : IBLogic<Question, QuestionListModel, QuestionDetailModel>
    {
        IQueryable<Question> SearchQuestions(string substring);
        IQueryable<Question> SortQuestions(IQueryable<Question> searchQuery, QuestionSortType type);
        Task<IEnumerable<QuestionListModel>> GetFilteredQuestions(string substring, QuestionSortType type);
    }
}
