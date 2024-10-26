using IW5.DAL.Initialization;

namespace IW5.DAL.Tests.Base
{
    public sealed class EnsureIW5DatabaseTestFixture
    {
        public EnsureIW5DatabaseTestFixture()
        {
            var configuration = TestHelpers.GetConfiguration();
            var context = TestHelpers.GetContext(configuration);
            SampleDataInitializer.ClearAndReseedDatabase(context);
            context.Dispose();
        }
    }
}