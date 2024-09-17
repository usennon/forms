using AutoMapper;
using IW5.API.DAL.Entities;
using IW5.Common.Enums;
using IW5.DAL.Entities.Interfaces;

namespace IW5.DAL.Entities
{
    public record UserEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public Role Role { get; set; }
        public ICollection<FormEntity> Form { get; set; } = [];
    }

    public class UserEntityMapperProfile : Profile
    {
        public UserEntityMapperProfile()
        {
            CreateMap<UserEntity, UserEntity>();
        }
    }
}