using IW5.BL.API.Contracts;
using IW5.BL.API;
using IW5.DAL.Contracts;
using AutoMapper;
using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using IW5.Models.Entities;

namespace IW5.BL.API
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserBLogic> _userService;
        private readonly Lazy<IFormBLogic> _formService;
        private readonly Lazy<IQuestionBLogic> _questionService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _userService = new Lazy<IUserBLogic>(() => new UserLogic(repositoryManager, mapper));
            _formService = new Lazy<IFormBLogic>(() => new FormLogic(repositoryManager, mapper));
            _questionService = new Lazy<IQuestionBLogic>(() => new QuestionLogic(repositoryManager, mapper));
        }

        public IUserBLogic UserService => _userService.Value;
        public IFormBLogic FormService => _formService.Value;
        public IQuestionBLogic QuestionService => _questionService.Value;

        public IBLogic<TEntity, TListModel, TDetailModel> GetService<TEntity, TListModel, TDetailModel>()
        where TEntity : BaseEntity
        where TListModel : ListModelBase
        where TDetailModel : DetailModelBase
        {
            if (typeof(TEntity) == typeof(Form))
                return (IBLogic<TEntity, TListModel, TDetailModel>)_formService.Value;
            if (typeof(TEntity) == typeof(User))
                return (IBLogic<TEntity, TListModel, TDetailModel>)_userService.Value;
            if (typeof(TEntity) == typeof(Question))
                return (IBLogic<TEntity, TListModel, TDetailModel>)_questionService.Value;

            throw new InvalidOperationException($"Сервис для типа {typeof(TEntity).Name} не найден.");
        }
    }
}
