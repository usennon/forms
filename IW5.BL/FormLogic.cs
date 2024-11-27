using AutoMapper;
using IW5.BL.API.Contracts;
using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using IW5.BL.Models.ManipulationModels.FormsModels;
using IW5.Common.Enums.Sorts;
using IW5.DAL.Contracts;
using IW5.DAL.Repository;
using IW5.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IW5.BL.API
{
    public class FormLogic :
        BaseLogic<Form, FormListModel, FormDetailModel, FormForManipulationModel>,
        IFormBLogic
    {

        public FormLogic(IRepositoryManager repositoryManager, IMapper mapper) 
            : base(repositoryManager, repositoryManager.Form, mapper)
        {
        }

        public async Task<FormDetailModel> GetFormByTitleAsync(string title)
        {
            var form = await _repositoryManager.Form.GetFormByTitleAsync(title, false);
            return _mapper.Map<FormDetailModel>(form);
        }

        private IQueryable<Form> SearchForms(string substring)
        {
            return _repositoryManager.Form.SearchFormByTitle(substring, false);
        }

        private IEnumerable<FormListModel> SortForms(IQueryable<Form> searchQuery, FormSortType type)
        {
            var result = _mapper.Map<List<FormListModel>>(searchQuery);;
            switch (type)
            {
                case FormSortType.AscendingTitle:
                    return result.OrderBy(e => e.Title);
                case FormSortType.DescendingTitle:
                    return result.OrderByDescending(e => e.Title);
                case FormSortType.AscendingStartDate:
                    return result.OrderBy(e => e.StartDate);
                case FormSortType.DescendingStartDate:
                    return result.OrderByDescending(e => e.StartDate);
                case FormSortType.AscendingEndDate:
                    return result.OrderBy(e => e.EndDate);
                case FormSortType.DescendingEndDate:
                    return result.OrderByDescending(e => e.EndDate);
                default: 
                    return result;
            }
        }

        public IEnumerable<FormListModel> GetFilteredForms(string substring = "", FormSortType type = FormSortType.None)
        {
            var searchedForms = SearchForms(substring);
            var filteredForms = SortForms(searchedForms, type);
            return filteredForms;
        }
        
    }
}

