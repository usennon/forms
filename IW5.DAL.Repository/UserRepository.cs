﻿using IW5.Models.Entities;
using IW5.DAL.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices.Marshalling;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace IW5.DAL.Repository
{
    public class UserRepository : BaseRepo<User>, IUserRepository
    {
        public UserRepository(FormsDbContext repositoryContext) : base(repositoryContext)
        {
            
        }
        public async Task<User> GetUserByNameAsync(string name, bool trackChanges)
            => await GetByCondition(e => e.Name.ToLower().Contains(name.ToLower()), trackChanges, f => f.Forms).SingleOrDefaultAsync();

        public IQueryable<User> SearchUserByName(string name, bool trackChanges)
            =>  GetByCondition(e => e.Name.ToLower().Contains(name.ToLower()), trackChanges, f => f.Forms);

    }
}