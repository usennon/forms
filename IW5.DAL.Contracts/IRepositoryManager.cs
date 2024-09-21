namespace IW5.DAL.Contracts
{
    public interface IRepositoryManager : IAsyncDisposable
    {
        IUserRepository User { get; }
        IFormRepository Form { get; }
        IQuestionRepository Question { get; }
        IOptionRepository Option { get; }

        Task SaveAsync();
    }
}
