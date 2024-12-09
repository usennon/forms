using IW5.Common.Enums;

namespace IW5.IdentityProvider.BL.Models;

public class AppUserDetailModel
{
    public Guid Id { get; set; }
    public string Subject { get; set; }
    public string UserName { get; set; }
    public Role Role { get; set; }
}
