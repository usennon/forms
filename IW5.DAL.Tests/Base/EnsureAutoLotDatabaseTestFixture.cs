using IW5.Dal.Initialization;

namespace IW5.Dal.Tests.Base
{
    public sealed class EnsureAutoLotDatabaseTestFixture : IDisposable
    {
        public EnsureAutoLotDatabaseTestFixture()
        {
            var configuration = TestHelpers.GetConfiguration();
            var context = TestHelpers.GetContext(configuration);
            SampleDataInitializer.ClearAndReseedDatabase(context);
            context.Dispose();
        }

        public void Dispose()
        {
        }
    }
}