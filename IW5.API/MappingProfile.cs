using AutoMapper;
using IW5.Models.Entities;
using IW5.BL.Models;
using IW5.Common.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace IW5.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // entity mappers
            CreateMap<User, User>();
            CreateMap<Form, Form>();
            CreateMap<Question, Question>();
            CreateMap<Option, Option>();

            // bl mappers

            // user mapper
            CreateMap<User, UserListModel>();
            CreateMap<User, UserDetailModel>().MapMember(dst => dst.Forms, src => src.Forms);
            CreateMap<UserDetailModel, User>().Ignore(dst => dst.Forms);

            // form mapper
            CreateMap<Form, FormListModel>();
            CreateMap<Form, FormDetailModel>().MapMember(dst => dst.Questions, src => src.Questions);
            CreateMap<FormDetailModel, Form>().Ignore(dst => dst.Questions);

            // question mapper
            CreateMap<Question, QuestionListModel>();
            CreateMap<Question, QuestionDetailModel>().MapMember(dst => dst.Options, src => src.Options);
            CreateMap<QuestionDetailModel, Question>().Ignore(dst => dst.Options);

            // option mapper
            CreateMap<Option, OptionListModel>();
        }
    }
}
