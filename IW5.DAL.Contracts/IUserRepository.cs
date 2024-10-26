using IW5.Models.Entities;

namespace IW5.DAL.Contracts
{
    public interface IUserRepository : IRepo<User>
    {
        Task<User> GetUserByNameAsync(string name, bool trackChanges);
        IQueryable<User> SearchUserByName(string name, bool trackChanges);
    }
}
