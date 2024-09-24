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
        protected override ICollection<string> NavigationPathDetail =>
            [$"{nameof(User.Forms)}"];
        public override async Task<User> GetById(Guid id)
        {
            var query = context.Users.AsQueryable();
            foreach(var detail in NavigationPathDetail)
            {
                query = _dbSet.Include(detail);
            }
            return await query.SingleOrDefaultAsync(entity => entity.Id == id);
        } 
        public async Task<IEnumerable<User>> GetAllUsersAsync(bool trackChanges) =>
            await GetAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();

    }
}
