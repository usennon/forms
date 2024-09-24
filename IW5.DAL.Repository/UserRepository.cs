using IW5.Models.Entities;
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

        public async Task<User> GetUserByIdAsync(Guid userId, bool trackChanges)
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

        public void CreateUser(User user) => Create(user);

        public void DeleteUser(User user) => Delete(user);

    }
}
