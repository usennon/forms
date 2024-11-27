using AutoMapper;
using IW5.BL.API.Contracts;
using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using IW5.BL.Models.ManipulationModels.QuestionModels;
using IW5.Common.Enums.Sorts;
using IW5.DAL.Contracts;
using IW5.DAL.Repository;
using IW5.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IW5.BL.API
{
    public class QuestionLogic :
        BaseLogic<Question, QuestionListModel, QuestionDetailModel, QuestionForManipulationModel>,
        IQuestionBLogic
    {
        public QuestionLogic(IRepositoryManager repositoryManager, IMapper mapper)
            : base(repositoryManager, repositoryManager.Question, mapper)
        {
        }

        private IQueryable<Question> SearchQuestions(string substring)
        {
            return _repositoryManager.Question.SearchQuestionByText(substring, false);
        }

        private IEnumerable<QuestionListModel> SortQuestions(IQueryable<Question> searchQuery, QuestionSortType type)
        {
            var result = _mapper.Map<List<QuestionListModel>>(searchQuery);
            switch (type)
            {
                case QuestionSortType.AscendingName:
                    return result.OrderBy(e => e.Text);
                case QuestionSortType.DescendingName:
                    return result.OrderByDescending(e => e.Text);
                default:
                    return result;
            }
        }

        public IEnumerable<QuestionListModel> GetFilteredQuestions(string substring = "", QuestionSortType type = QuestionSortType.None)
        {
            var searchedQuestions = SearchQuestions(substring);
            var filteredQuestions = SortQuestions(searchedQuestions, type);
            return filteredQuestions;
        }
    }
}

