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
        private readonly IRepositoryManager _repositoryManager;
        private readonly IRepo<TEntity> _baseRepository;
        private readonly IMapper _mapper;

        public BaseLogic(IRepositoryManager repositoryManager, IRepo<TEntity> baseRepository, IMapper mapper)
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
            var entity = await _baseRepository.GetByIdAsync(id, false);
            return _mapper.Map<TDetailModel>(entity);
        }

        public async Task CreateOrUpdateAsync(TDetailModel model)
        {

                if (await _baseRepository.ExistsAsync(model.Id))
                {
                    await UpdateAsync(model.Id, model, true);
                }
                else
                {
                    Create(model);
                }
                await _repositoryManager.SaveAsync();
        }

        public async Task<TDetailModel> Create(TDetailModel model)
        {
            var entity = _mapper.Map<TEntity>(model);

            _baseRepository.Create(entity);
            await _repositoryManager.SaveAsync();

            var entityToReturn = _mapper.Map<TDetailModel>(entity);

            return entityToReturn;
        }

        public async Task UpdateAsync(Guid id, TDetailModel dtoModel, bool trackChanges)
        {
            var entity = await _baseRepository.GetByIdAsync(id, trackChanges);
            if (entity is null)
                throw new Exception();

            _mapper.Map(dtoModel, entity);

            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteAsync(Guid id, bool trackChanges)
        {
            try
            {
                var entity = await _baseRepository.GetByIdAsync(id, trackChanges);
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
