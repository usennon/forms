using AutoMapper;
using IW5.Models.Entities;
using IW5.Common.Extensions;

namespace IW5.BL.Models.ModelMappers
{
    public class OptionMapperProfile : Profile
    {
        public OptionMapperProfile()
        {
            CreateMap<Option, OptionListModel>();

        }
    }
}
