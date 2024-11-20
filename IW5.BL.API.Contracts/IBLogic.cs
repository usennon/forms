using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using IW5.BL.Models.ManipulationModels;
using IW5.Models.Entities;
using System.Runtime.InteropServices;

namespace IW5.BL.API.Contracts
{
    public interface IBLogic<TEntity, TListModel, TDetailModel, TManipulationModel> : ILogic
        where TEntity : BaseEntity
        where TListModel : ListModelBase
        where TDetailModel : DetailModelBase
        where TManipulationModel : IManipulationModel
    {
        List<TListModel> GetAll();
        Task<TDetailModel?> GetByIdAsync(Guid id);
        Task<TDetailModel> Create(TManipulationModel model);
        Task UpdateAsync(Guid id, TManipulationModel model, bool trackChanges);
        Task DeleteAsync(Guid id, bool trackChanges);
    }
}
