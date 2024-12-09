using Microsoft.AspNetCore.Identity;

namespace IW5.IdentityProvider.DAL.Entities;

public class AppRoleEntity : IdentityRole<Guid>
{
    public AppRoleEntity(string roleName) : base(roleName)
    {
    }
    public AppRoleEntity() : base()
    {
    }
}