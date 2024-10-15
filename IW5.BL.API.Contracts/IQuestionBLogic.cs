using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using IW5.Models.Entities;
using IW5.Common.Enums.Sorts;

namespace IW5.BL.API.Contracts
{
    public interface IQuestionBLogic : IBLogic<Question, QuestionListModel, QuestionDetailModel>
    {
        Task<IEnumerable<QuestionListModel>> GetFilteredQuestions(string substring, QuestionSortType type);
    }
}
