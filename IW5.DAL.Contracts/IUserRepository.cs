using IW5.Models.Entities;

namespace IW5.DAL.Contracts
{
    public interface IUserRepository : IRepo<User>
    {
        Task<IEnumerable<User>?> GetUserByNameAsync(string name, bool trackChanges);
    }
}
