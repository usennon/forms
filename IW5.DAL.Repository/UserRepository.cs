using IW5.Models.Entities;
using IW5.DAL.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices.Marshalling;


namespace IW5.DAL.Repository
{
    public class UserRepository : BaseRepo<User>, IUserRepository
    {
        public UserRepository(FormsDbContext repositoryContext) : base(repositoryContext) 
        {
        }

        public async Task<User> GetUserById(Guid userId, bool trackChanges)
        {
            return await GetByCondition(
                u => u.Id.Equals(userId),
                trackChanges, u => u.Forms)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(bool trackChanges) =>
            await GetAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();

    }
}
