using IW5.Models.Entities;

namespace IW5.DAL.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync(bool trackChanges);
    }
}
