using AutoMapper;
using IW5.Models.Entities;
using IW5.Common.Extensions;

namespace IW5.BL.Models.ModelMappers
{
    public class QuestionMapperProfile : Profile
    {
        public QuestionMapperProfile()
        {
            CreateMap<Question, QuestionListModel>();
            CreateMap<Question, QuestionDetailModel>().MapMember(dst => dst.Options, src => src.Options);
            CreateMap<QuestionDetailModel, Question>().Ignore(dst => dst.Options);

        }
    }
}
