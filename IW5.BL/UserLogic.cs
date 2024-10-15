﻿using AutoMapper;
using IW5.BL.API.Contracts;
using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using IW5.Common.Enums.Sorts;
using IW5.DAL.Contracts;
using IW5.DAL.Repository;
using IW5.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IW5.BL.API
{    
    public class UserLogic : 
        BaseLogic<User, UserListModel, UserDetailModel>, 
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

        private IQueryable<User> SortUsers(IQueryable<User> searchQuery, UserSortType type)
        {
            switch (type) 
            {
                case UserSortType.AscendingName:
                    return searchQuery.OrderBy(e => e.Name);
                case UserSortType.DescendingName:
                    return searchQuery.OrderByDescending(e => e.Name);
                default: return searchQuery;
            }
        }

        public async Task<IEnumerable<UserListModel>> GetFilteredUsers(string substring = "", UserSortType type = UserSortType.None)
        {
            var searchedUsers = SearchUsers(substring);
            var filteredUsers = SortUsers(searchedUsers, type);
            var result = await filteredUsers.ToListAsync();
            return _mapper.Map<List<UserListModel>>(result);
        }
    }
}
