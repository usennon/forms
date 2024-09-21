using IW5.Models.Entities;
using IW5.DAL.Contracts;
using Microsoft.EntityFrameworkCore;


namespace IW5.DAL.Repository
{
    public class UserRepository : BaseRepo<User>, IUserRepository
    {
        public UserRepository(FormsDbContext repositoryContext) : base(repositoryContext) 
        {
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync(bool trackChanges) =>
            await GetAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();

    }
}
