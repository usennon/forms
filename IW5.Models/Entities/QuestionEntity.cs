using IW5.DAL.Contracts;
using IW5.Common.Enums;

namespace IW5.Models.Entities
{
    public record QuestionEntity : BaseEntity
    {
        public FormEntity? Form { get; init; }
        public QuestionType Type { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }

    //public class QuestionEntityMapperProfile : Profile
    //{
    //    public QuestionEntityMapperProfile()
    //    {
    //        CreateMap<QuestionEntity, QuestionEntity>();
    //    }
    //}
}

