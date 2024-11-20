using IW5.BL.API.Contracts;
using IW5.IdentityProvider.BL.Models;

namespace IW5.IdentityProvider.BL.Facades;

public interface IAppUserClaimsFacade : ILogic
{
    Task<IEnumerable<AppUserClaimListModel>> GetAppUserClaimsByUserIdAsync(Guid userId);
}
