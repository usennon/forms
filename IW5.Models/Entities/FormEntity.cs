using IW5.DAL.Contracts;

namespace IW5.Models.Entities
{
    public record FormEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public UserEntity? Author { get; set; }
        public ICollection<QuestionEntity> Questions { get; set; } = [];
        public DateTime Start {  get; set; }
        public DateTime End { get; set; }
        

    }

    //public class FormEntityMapperProfile : Profile
    //{
    //    public FormEntityMapperProfile()
    //    {
    //        CreateMap<FormEntity, FormEntity>();
    //    }
    //}
}
