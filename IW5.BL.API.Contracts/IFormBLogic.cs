using IW5.BL.Models;
using IW5.Common.Enums.Sorts;
using IW5.Models.Entities;

namespace IW5.BL.API.Contracts
{
    public interface IFormBLogic : IBLogic<Form, FormListModel, FormDetailModel>
    {
        IQueryable<Form> SearchForms(string substring);
        IQueryable<Form> SortForms(IQueryable<Form> searchQuery, FormSortType type);
        Task<IEnumerable<FormListModel>> GetFilteredForms(string substring, FormSortType type);
    }
}
