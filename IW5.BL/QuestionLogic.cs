using AutoMapper;
using IW5.BL.API.Contracts;
using IW5.BL.Models;
using IW5.Common.Enums.Sorts;
using IW5.DAL.Contracts;
using IW5.DAL.Repository;
using IW5.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IW5.BL.API
{
    public class QuestionLogic :
        BaseLogic<Question, QuestionListModel, QuestionDetailModel>,
        IQuestionBLogic
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public QuestionLogic(RepositoryManager repositoryManager, IQuestionRepository questionRepository, IMapper mapper)
            : base(repositoryManager, questionRepository, mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        private IQueryable<Question> SearchQuestions(string substring)
        {
            return _questionRepository.SearchQuestionByText(substring, false);
        }

        private IQueryable<Question> SortQuestions(IQueryable<Question> searchQuery, QuestionSortType type)
        {
            switch (type)
            {
                case QuestionSortType.AscendingName:
                    return searchQuery.OrderBy(e => e.Text);
                case QuestionSortType.DescendingName:
                    return searchQuery.OrderByDescending(e => e.Text);
                default:
                    return searchQuery;
            }
        }

        public async Task<IEnumerable<QuestionListModel>> GetFilteredQuestions(string substring = "", QuestionSortType type = QuestionSortType.None)
        {
            var searchedQuestions = SearchQuestions(substring);
            var filteredQuestions = SortQuestions(searchedQuestions, type);
            var result = await filteredQuestions.ToListAsync();
            return _mapper.Map<List<QuestionListModel>>(result);
        }
    }
}

