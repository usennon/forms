using AutoMapper;
using IW5.Models.Entities;
using IW5.Common.Extensions;

namespace IW5.BL.Models.ModelMappers
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile() 
        {
            CreateMap<User, UserListModel>();
            CreateMap<User, UserDetailModel>().MapMember(dst => dst.Forms, src => src.Forms);
            CreateMap<UserDetailModel, User>().Ignore(dst => dst.Forms);
                
        }
    }
}
