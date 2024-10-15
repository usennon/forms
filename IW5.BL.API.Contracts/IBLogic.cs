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
        void Create(TDetailModel model);
        Task UpdateAsync(TDetailModel model);
        void Delete(TDetailModel model);
    }
}
