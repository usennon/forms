using AutoMapper;
using IW5.Models.Entities;
using IW5.Common.Extensions;

namespace IW5.BL.Models.ModelMappers
{
    public class FormMapperProfile : Profile
    {
        public FormMapperProfile()
        {
            CreateMap<Form, FormListModel>();
            CreateMap<Form, FormDetailModel>().MapMember(dst => dst.Questions, src => src.Questions);
            CreateMap<FormDetailModel, Form>().Ignore(dst => dst.Questions);

        }
    }
}
