using AutoMapper;
using IW5.Common.Extensions;
using IW5.BL.Models.ListModels;
using IW5.IdentityProvider.BL.Models;
using IW5.IdentityProvider.DAL.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using IW5.Models.Entities;

namespace IW5.IdentityProvider.BL.MapperProfiles;

public class AppUserMapperProfile : Profile
{
    public AppUserMapperProfile()
    {
        CreateMap<AppUserCreateModel, User>()
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
            .Ignore(entity => entity.AccessFailedCount)
            .Ignore(entity => entity.PhotoUrl)
            .Ignore(entity => entity.Forms)
            .Ignore(entity => entity.CreatedAt)
            .Ignore(entity => entity.Role);

        CreateMap<User, AppUserDetailModel>();
        CreateMap<User, UserListModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName))
            .Ignore(user => user.PhotoUrl)
            .Ignore(user => user.Role);
    }
}
