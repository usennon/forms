using AutoMapper;
using IW5.DAL.Repository;
using IW5.BL.API.Contracts;
using IW5.DAL.Contracts;
using IW5.BL.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using IW5.Models.Entities;

namespace IW5.BL.API
{
    public abstract class BaseLogic<TEntity, TListModel, TDetailModel> : IBLogic<TEntity, TListModel, TDetailModel>
        where TEntity : BaseEntity, new()
        where TListModel : class, IModel
        where TDetailModel : class, IModel
    {
        private readonly RepositoryManager _repositoryManager;
        private readonly IRepo<TEntity> _baseRepository;
        private readonly IMapper _mapper;

        public BaseLogic(RepositoryManager repositoryManager, IRepo<TEntity> baseRepository, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public List<TListModel> GetAll()
        {
            var list = _baseRepository.GetAll(false);
            return _mapper.Map<List<TListModel>>(list);
        }

        public async Task<TDetailModel?> GetByIdAsync(Guid id)
        {
            var ingredientEntity = await _baseRepository.GetByIdAsync(id, false);
            return _mapper.Map<TDetailModel>(ingredientEntity);
        }

        public async Task CreateOrUpdateAsync(TDetailModel model)
        {
            try
            {
                if (await _baseRepository.ExistsAsync(model.Id))
                {
                    await UpdateAsync(model);
                }
                else
                {
                    Create(model);
                }
                await _repositoryManager.SaveAsync();
            }
            catch (Exception)
            {
                await _repositoryManager.DisposeAsync();
                throw;
            }
        }

        public void Create(TDetailModel ingredientModel)
        {
            var ingredientEntity = _mapper.Map<TEntity>(ingredientModel);
            _baseRepository.Create(ingredientEntity);
        }

        public async Task UpdateAsync(TDetailModel ingredientModel)
        {
            var ingredientEntity = _mapper.Map<TEntity>(ingredientModel);
            await _baseRepository.UpdateAsync(ingredientEntity);
        }

        public async void Delete(TDetailModel model)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(model);
                if (entity != null)
                {
                    _baseRepository.Delete(entity);
                    await _repositoryManager.SaveAsync();
                }
            }
            catch (Exception)
            {
                await _repositoryManager.DisposeAsync();
                throw;
            }

        }
    }
}
