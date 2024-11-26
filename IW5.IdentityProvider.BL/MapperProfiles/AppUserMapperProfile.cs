using AutoMapper;
using IW5.Common.Extensions;
using IW5.BL.Models.ListModels;
using IW5.IdentityProvider.BL.Models;
using IW5.IdentityProvider.DAL.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IW5.IdentityProvider.BL.MapperProfiles;

public class AppUserMapperProfile : Profile
{
    public AppUserMapperProfile()
    {
        CreateMap<AppUserCreateModel, AppUserEntity>()
            .Ignore(entity => entity.Active)
            .Ignore(entity => entity.Id)
            .Ignore(entity => entity.NormalizedUserName)
            .Ignore(entity => entity.NormalizedEmail)
            .Ignore(entity => entity.EmailConfirmed)
            .Ignore(entity => entity.PasswordHash)
            .Ignore(entity => entity.SecurityStamp)
            .Ignore(entity => entity.ConcurrencyStamp)
            .Ignore(entity => entity.PhoneNumber)
            .Ignore(entity => entity.PhoneNumberConfirmed)
            .Ignore(entity => entity.TwoFactorEnabled)
            .Ignore(entity => entity.LockoutEnd)
            .Ignore(entity => entity.LockoutEnabled)
            .Ignore(entity => entity.AccessFailedCount);

        CreateMap<AppUserEntity, AppUserDetailModel>();
        CreateMap<AppUserEntity, UserListModel>()
            .Ignore(user => user.PhotoUrl);
    }
}
