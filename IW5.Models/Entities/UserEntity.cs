using IW5.Common.Enums;

namespace IW5.Models.Entities
{
    public record UserEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public Role Role { get; set; }
        public ICollection<FormEntity> Form { get; set; } = [];
    }

    //public class UserEntityMapperProfile : Profile
    //{
    //    public UserEntityMapperProfile()
    //    {
    //        CreateMap<UserEntity, UserEntity>();
    //    }
    //}
}