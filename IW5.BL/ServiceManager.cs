using IW5.BL.API.Contracts;
using IW5.BL.API;
using IW5.DAL.Contracts;
using AutoMapper;

namespace IW5.BL.API
{
    public class ServiceManager : IServiceManager
    {

        private readonly Lazy<IUserBLogic> _userService;
        private readonly Lazy<IFormBLogic> _formService;
        private readonly Lazy<IQuestionBLogic> _questionService;
        private readonly Lazy<IOptionBLogic> _optionService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _userService = new Lazy<IUserBLogic>(() => new UserLogic(repositoryManager, mapper));
            _formService = new Lazy<IFormBLogic>(() => new FormLogic(repositoryManager, mapper));
            _questionService = new Lazy<IQuestionBLogic>(() => new QuestionLogic(repositoryManager, mapper));
            _optionService = new Lazy<IOptionBLogic>(() => new OptionLogic(repositoryManager, mapper));
        }

        public IUserBLogic UserService => _userService.Value;
        public IFormBLogic FormService => _formService.Value;
        public IQuestionBLogic QuestionService => _questionService.Value;
        public IOptionBLogic OptionService => _optionService.Value;
    }
}
