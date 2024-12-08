using IW5.IdentityProvider.DAL.Entities;
using IW5.Models.Entities;

namespace IW5.IdentityProvider.DAL.Repositories;

public interface IAppUserRepository
{
    Task<IList<User>> SearchAsync(string searchString);
}
