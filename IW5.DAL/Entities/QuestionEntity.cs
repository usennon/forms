using AutoMapper;
using IW5.DAL.Entities.Interfaces;
using IW5.Common.Enums;
using IW5.API.DAL.Entities;

namespace IW5.DAL.Entities
{
    public record QuestionEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public FormEntity? Form { get; init; }
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

