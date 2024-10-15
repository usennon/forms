using IW5.DAL.Initialization;

namespace IW5.BL.Tests.Base
{
    public sealed class EnsureIW5DatabaseTestFixture
    {
        public EnsureIW5DatabaseTestFixture()
        {
            var configuration = TestHelpers.GetConfiguration();
            var context = TestHelpers.GetContext(configuration);
            SampleDataInitializer.ClearAndReseedDatabase(context);
            Thread.Sleep(5000);
            context.Dispose();
        }
    }
}