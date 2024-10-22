using AutoMapper;
using IW5.Models.Entities;
using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using IW5.Common.Extensions;
using IW5.BL.Models.ManipulationModels.UserModels;
using IW5.BL.Models.ManipulationModels.FormsModels;
using IW5.BL.Models.ManipulationModels.QuestionModels;
using IW5.BL.Models.ManipulationModels.OptionModels;

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
            CreateMap<UserForManipulationDTO, User>().ReverseMap();

            // form mapper
            CreateMap<Form, FormListModel>();
            CreateMap<Form, FormDetailModel>().MapMember(dst => dst.Questions, src => src.Questions);
            CreateMap<FormDetailModel, Form>().Ignore(dst => dst.Questions);
            CreateMap<FormForManipulationDTO, Form>().ReverseMap();

            // question mapper
            CreateMap<Question, QuestionListModel>();
            CreateMap<Question, QuestionDetailModel>().MapMember(dst => dst.Options, src => src.Options);
            CreateMap<QuestionDetailModel, Question>().Ignore(dst => dst.Options);
            CreateMap<UserForManipulationDTO, User>().ReverseMap();
            CreateMap<QuestionForManipulationDTO, Question>().ReverseMap();

            // option mapper
            CreateMap<Option, OptionListModel>();
            CreateMap<OptionForManipulationDTO, Option>().ReverseMap();
            CreateMap<Option, OptionDetailModel>();
        }
    }
}
