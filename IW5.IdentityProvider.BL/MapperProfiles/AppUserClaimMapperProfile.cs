using System.Security.Claims;
using AutoMapper;
using IW5.IdentityProvider.BL.Models;

namespace CookBook.IdentityProvider.BL.MapperProfiles;

public class AppUserClaimMapperProfile : Profile
{
    public AppUserClaimMapperProfile()
    {
        CreateMap<Claim, AppUserClaimListModel>()
            .ForMember(dest => dest.ClaimType, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.ClaimValue, opt => opt.MapFrom(src => src.Value));
    }
}
