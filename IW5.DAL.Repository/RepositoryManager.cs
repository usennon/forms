using IW5.DAL.Contracts;

namespace IW5.DAL.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly FormsDbContext _repositoryContext;
        public readonly Lazy<IUserRepository> _userRepository;
        public readonly Lazy<IFormRepository> _formRepository;
        public readonly Lazy<IQuestionRepository> _questionRepository;
        public readonly Lazy<IOptionRepository> _optionRepository;

        public RepositoryManager(FormsDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryContext));
            _formRepository = new Lazy<IFormRepository>(() => new FormRepository(repositoryContext));
            _optionRepository = new Lazy<IOptionRepository>(() => new OptionRepository(repositoryContext));
            _questionRepository = new Lazy<IQuestionRepository>(() => new QuestionRepository(repositoryContext));
        }

        public IFormRepository Form => _formRepository.Value;
        public IUserRepository User => _userRepository.Value;
        public IQuestionRepository Question => _questionRepository.Value;
        public IOptionRepository Option => _optionRepository.Value;


        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
        public async ValueTask DisposeAsync() => await _repositoryContext.DisposeAsync().ConfigureAwait(false);



    }
}
