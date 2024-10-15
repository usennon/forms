﻿using AutoMapper;
using IW5.BL.API.Contracts;
using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using IW5.Common.Enums.Sorts;
using IW5.DAL.Contracts;
using IW5.DAL.Repository;
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

        public FormLogic(IRepositoryManager repositoryManager, IMapper mapper) 
            : base(repositoryManager, repositoryManager.Form, mapper)
        {
            _formRepository = repositoryManager.Form;
            _mapper = mapper;
        }

        public async Task<FormDetailModel> GetFormByTitleAsync(string title)
        {
            var form = await _formRepository.GetFormByTitleAsync(title, false);
            return _mapper.Map<FormDetailModel>(form);
        }

        private IQueryable<Form> SearchForms(string substring)
        {
            return _formRepository.SearchFormByTitle(substring, false);
        }

        private IQueryable<Form> SortForms(IQueryable<Form> searchQuery, FormSortType type)
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

