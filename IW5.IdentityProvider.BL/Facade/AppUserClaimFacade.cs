using AutoMapper;
using IW5.IdentityProvider.BL.Facades;
using IW5.IdentityProvider.BL.Models;
using IW5.IdentityProvider.DAL.Entities;
using IW5.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace IW5.IdentityProvider.BL.Facades;

public class AppUserClaimsFacade : IAppUserClaimsFacade
{
    private readonly UserManager<User> userManager;
    private readonly IMapper mapper;

    public AppUserClaimsFacade(
        UserManager<User> userManager,
        IMapper mapper)
    {
        this.userManager = userManager;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<AppUserClaimListModel>> GetAppUserClaimsByUserIdAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return new List<AppUserClaimListModel>();
        }
        else
        {
            var claims = await userManager.GetClaimsAsync(user);
            return claims.Select(claim => mapper.Map<AppUserClaimListModel>(claim)).ToList();
        }
    }
}
