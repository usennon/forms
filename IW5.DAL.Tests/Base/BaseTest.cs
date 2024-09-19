using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using IW5.DAL;

namespace IW5.Dal.Tests.Base
{
    public abstract class BaseTest : IDisposable
    {
        protected readonly IConfiguration Configuration;
        protected readonly FormsDbContext Context;

        protected BaseTest()
        {
            Configuration = TestHelpers.GetConfiguration();
            Context = TestHelpers.GetContext(Configuration);
        }

        public virtual void Dispose()
        {
            Context.Dispose();
        }
        protected void ExecuteInATransaction(Action actionToExecute)
        {
            var strategy = Context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using var trans = Context.Database.BeginTransaction();
                actionToExecute();
                trans.Rollback();
            });
        }

        protected void ExecuteInASharedTransaction(Action<IDbContextTransaction> actionToExecute)
        {
            var strategy = Context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using IDbContextTransaction trans = Context.Database.BeginTransaction(IsolationLevel.ReadUncommitted);
                actionToExecute(trans);
                trans.Rollback();
            });
        }
    }
}