using AutoMapper;
using IW5.BL.API.Contracts;
using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using IW5.BL.Models.ManipulationModels;
using IW5.BL.Models.ManipulationModels.UserModels;
using IW5.Common.Enums;
using IW5.Common.Enums.Sorts;
using IW5.DAL.Contracts;
using IW5.IdentityProvider.DAL.Entities;
using IW5.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace IW5.BL.API
{    
    public class UserLogic : 
        BaseLogic<User, UserListModel, UserDetailModel, UserForManipulationModel>, 
        IUserBLogic
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<AppRoleEntity> _roleManager;
        public UserLogic(UserManager<User> userManager, RoleManager<AppRoleEntity> roleManager, IRepositoryManager repositoryManager, IMapper mapper)
            : base(repositoryManager, repositoryManager.User, mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        private IQueryable<User> SearchUsers(string substring)
        {
            return _repositoryManager.User.SearchUserByName(substring, false);
        }

        private IEnumerable<UserListModel> SortUsers(IQueryable<User> searchQuery, UserSortType type)
        {
            var result = _mapper.Map<IEnumerable<UserListModel>>(searchQuery);
            switch (type) 
            {
                case UserSortType.AscendingName:
                    return result.OrderBy(e => e.Name);
                case UserSortType.DescendingName:
                    return result.OrderByDescending(e => e.Name);
                default:
                    return result;
            }
        }

        public override async Task<UserDetailModel> Create(UserForManipulationModel model)
        {
            var entity = _mapper.Map<User>(model);
            var isExists = await _repositoryManager.User.GetUserByNameAsync(model.UserName, false);
            if (isExists != null) {
                throw new Exception("User is already exists");
            }
            entity.Id = Guid.NewGuid();
            await _userManager.CreateAsync(entity, model.Password);
            await _repositoryManager.SaveAsync();
            var entityToReturn = _mapper.Map<UserDetailModel>(entity);
            return entityToReturn;
        }

        public override async Task UpdateAsync(Guid id, UserForManipulationModel dtoModel, bool trackChanges)
        {
            var entity = await _baseRepository.GetByIdAsync(id, trackChanges);
            if (entity is null)
                throw new KeyNotFoundException($"Entity with id {id} not found!");
            _mapper.Map(dtoModel, entity);
            await _repositoryManager.User.UpdateAsync(entity);
            await _repositoryManager.SaveAsync();
        }

        public IEnumerable<UserListModel> GetFilteredUsers(string substring = "", UserSortType type = UserSortType.None)
        {
            var searchedUsers = SearchUsers(substring);
            var filteredUsers = SortUsers(searchedUsers, type);
            return filteredUsers;

        }
    }
}
