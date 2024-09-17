using AutoMapper;
using IW5.DAL.Entities.Interfaces;
using IW5.Common.Enums;

namespace IW5.DAL.Entities
{
    public record QuestionEntity : IEntity
    {
        public Guid Id { get; set; }
        public QuestionType Type { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }

    public class QuestionEntityMapperProfile : Profile
    {
        public QuestionEntityMapperProfile()
        {
            CreateMap<QuestionEntity, QuestionEntity>();
        }
    }
}

