using AutoMapper;
using IW5.DAL.Repository;
using IW5.BL.API.Contracts;
using IW5.DAL.Contracts;
using IW5.BL.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using IW5.Models.Entities;
using IW5.BL.Models.ListModels;
using IW5.BL.Models.DetailModels;

namespace IW5.BL.API
{
    public abstract class BaseLogic<TEntity, TListModel, TDetailModel> : IBLogic<TEntity, TListModel, TDetailModel>
        where TEntity : BaseEntity, new()
        where TListModel : ListModelBase
        where TDetailModel : DetailModelBase
    {
        private readonly IRepo<TEntity> _baseRepository;
        private readonly IMapper _mapper;

        public BaseLogic(IRepo<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public List<TListModel> GetAll()
        {
            return _mapper.Map<List<TListModel>>(_baseRepository.GetAll(false));
        }

        public async Task<TDetailModel?> GetByIdAsync(Guid id)
        {
            var ingredientEntity = await _baseRepository.GetByIdAsync(id, false);
            return _mapper.Map<TDetailModel>(ingredientEntity);
        }

        public async Task CreateOrUpdate(TDetailModel model, Guid id)
        {
            if (await _baseRepository.ExistsAsync(id)){
                Update(model);
                return;
            }
            Create(model);
        }

        public void Create(TDetailModel ingredientModel)
        {
            var ingredientEntity = _mapper.Map<TEntity>(ingredientModel);
            _baseRepository.Create(ingredientEntity);
        }

        public void Update(TDetailModel ingredientModel)
        {
            var ingredientEntity = _mapper.Map<TEntity>(ingredientModel);
            _baseRepository.Create(ingredientEntity);
        }

        public void Delete(TDetailModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            _baseRepository.Delete(entity);
        }
    }
}
