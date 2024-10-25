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
            _context = TestHelpers.GetContext(configuration); 
            SampleDataInitializer.ClearAndReseedDatabase(_context); 
        }
        public void Dispose()
        {
            _context.Dispose(); 
        }
        public FormsDbContext GetContext() => _context; 
    }
}