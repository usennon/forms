using IW5.Models.Entities;
using IW5.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IW5.BL.API.Contracts;
using AutoMapper;
using IW5.DAL.Contracts;
using IW5.DAL.Repository;

namespace IW5.BL.API
{
    public class OptionLogic : BaseLogic<Option, OptionListModel, OptionListModel>, IOptionBLogic
    {
        private readonly IOptionRepository _optionRepository;
        private readonly IMapper _mapper;

        public OptionLogic(RepositoryManager repositoryManager, IOptionRepository optionRepository, IMapper mapper) 
            : base(repositoryManager, optionRepository, mapper)
        {
            _optionRepository = optionRepository;
            _mapper = mapper;
        }
    }
}
