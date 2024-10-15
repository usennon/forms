using IW5.DAL;
using IW5.DAL.Initialization;

namespace IW5.BL.Tests.Base
{
    public sealed class EnsureIW5DatabaseTestFixture : IDisposable
    {
        private readonly FormsDbContext _context;

        public EnsureIW5DatabaseTestFixture()
        {
            var configuration = TestHelpers.GetConfiguration();
            _context = TestHelpers.GetContext(configuration); // Создаем контекст
            SampleDataInitializer.ClearAndReseedDatabase(_context); // Инициализация данных
        }

        // Вызываем Dispose в конце всех тестов
        public void Dispose()
        {
            _context.Dispose(); // Уничтожаем контекст после завершения всех тестов
        }

        public FormsDbContext GetContext() => _context; // Предоставляем доступ к контексту для тестов
    }
}