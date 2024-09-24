﻿using IW5.Models.Entities;

namespace IW5.DAL.Contracts
{
    public interface IUserRepository : IRepo<User>
    {
        Task<User> GetUserByIdAsync(Guid userId, bool trackChanges);
        Task<IEnumerable<User>> GetAllUsersAsync(bool trackChanges);
        void CreateUser(User user);
        void DeleteUser(User user);
    }
}
