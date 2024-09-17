using AutoMapper;
using IW5.API.DAL.Entities;
using IW5.DAL.Entities.Interfaces;

namespace IW5.DAL.Entities
{
    public record FormEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public UserEntity? Author { get; set; }
        public ICollection<QuestionEntity> Questions { get; set; } = [];
        public DateTime Start {  get; set; }
        public DateTime End { get; set; }
        

    }

    public class FormEntityMapperProfile : Profile
    {
        public FormEntityMapperProfile()
        {
            CreateMap<FormEntity, FormEntity>();
        }
    }
}
