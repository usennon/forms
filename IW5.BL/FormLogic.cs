using AutoMapper;
using IW5.BL.API.Contracts;
using IW5.BL.Models;
using IW5.Common.Enums.Sorts;
using IW5.DAL.Contracts;
using IW5.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IW5.BL.API
{
    public class FormLogic :
        BaseLogic<Form, FormListModel, FormDetailModel>,
        IFormBLogic
    {
        private readonly IFormRepository _formRepository;
        private readonly IMapper _mapper;

        public FormLogic(IFormRepository formRepository, IMapper mapper) : base(formRepository, mapper)
        {
            _formRepository = formRepository;
            _mapper = mapper;
        }

        public IQueryable<Form> SearchForms(string substring)
        {
            return _formRepository.SearchFormByTitle(substring, false);
        }

        public IQueryable<Form> SortForms(IQueryable<Form> searchQuery, FormSortType type)
        {
            switch (type)
            {
                case FormSortType.AscendingTitle:
                    return searchQuery.OrderBy(e => e.Title);
                case FormSortType.DescendingTitle:
                    return searchQuery.OrderByDescending(e => e.Title);
                case FormSortType.AscendingStartDate:
                    return searchQuery.OrderBy(e => e.StartDate);
                case FormSortType.DescendingStartDate:
                    return searchQuery.OrderByDescending(e => e.StartDate);
                case FormSortType.AscendingEndDate:
                    return searchQuery.OrderBy(e => e.EndDate);
                case FormSortType.DescendingEndDate:
                    return searchQuery.OrderByDescending(e => e.EndDate);
                default: 
                    return searchQuery;
            }
        }

        public async Task<IEnumerable<FormListModel>> GetFilteredForms(string substring = "", FormSortType type = FormSortType.None)
        {
            var searchedForms = SearchForms(substring);
            var filteredForms = SortForms(searchedForms, type);
            var result = await filteredForms.ToListAsync();
            return _mapper.Map<List<FormListModel>>(result);
        }
    }
}

