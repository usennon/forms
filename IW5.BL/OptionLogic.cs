using IW5.Models.Entities;
using IW5.BL.Models.ListModels;
using IW5.BL.Models.DetailModels;
using IW5.BL.API.Contracts;
using AutoMapper;
using IW5.DAL.Contracts;
using IW5.DAL.Repository;
using IW5.BL.Models.ManipulationModels.OptionModels;

namespace IW5.BL.API
{
    public class OptionLogic : BaseLogic<Option, OptionListModel, OptionDetailModel, OptionForManipulationDTO>, IOptionBLogic
    {
        private readonly IOptionRepository _optionRepository;
        private readonly IMapper _mapper;

        public OptionLogic(IRepositoryManager repositoryManager, IMapper mapper) 
            : base(repositoryManager, repositoryManager.Option, mapper)
        {
            _optionRepository = repositoryManager.Option;
            _mapper = mapper;
        }
    }
}
