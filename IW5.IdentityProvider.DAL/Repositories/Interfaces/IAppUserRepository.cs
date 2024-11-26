using IW5.IdentityProvider.DAL.Entities;

namespace IW5.IdentityProvider.DAL.Repositories;

public interface IAppUserRepository
{
    Task<IList<AppUserEntity>> SearchAsync(string searchString);
}
