using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using IW5.Models.Entities;
using System.Runtime.InteropServices;

namespace IW5.BL.API.Contracts
{
    public interface IBLogic<TEntity, TListModel, TDetailModel> 
        where TEntity : BaseEntity
        where TListModel : ListModelBase
        where TDetailModel : DetailModelBase
    {
        List<TListModel> GetAll();
        Task<TDetailModel?> GetByIdAsync(Guid id);
        Task CreateOrUpdateAsync(TDetailModel model);
        Task<TDetailModel> Create(TDetailModel model);
        Task UpdateAsync(Guid id, TDetailModel model, bool trackChanges);
        Task DeleteAsync(Guid id, bool trackChanges);
    }
}
