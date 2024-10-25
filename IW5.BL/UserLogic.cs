using AutoMapper;
using IW5.BL.API.Contracts;
using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using IW5.BL.Models.ManipulationModels.UserModels;
using IW5.Common.Enums.Sorts;
using IW5.DAL.Contracts;
using IW5.DAL.Repository;
using IW5.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IW5.BL.API
{    
    public class UserLogic : 
        BaseLogic<User, UserListModel, UserDetailModel, UserForManipulationDTO>, 
        IUserBLogic
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserLogic(IRepositoryManager repositoryManager, IMapper mapper)
            : base(repositoryManager, repositoryManager.User, mapper)
        {
            _userRepository = repositoryManager.User;
            _mapper = mapper;
        }

        private IQueryable<User> SearchUsers(string substring)
        {
            return _userRepository.SearchUserByName(substring, false);
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

        public IEnumerable<UserListModel> GetFilteredUsers(string substring = "", UserSortType type = UserSortType.None)
        {
            var searchedUsers = SearchUsers(substring);
            var filteredUsers = SortUsers(searchedUsers, type);
            return filteredUsers;

        }
    }
}
