using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using IW5.DAL;
using IW5.DAL.Repository;
using IW5.BL.Tests;

namespace IW5.BL.Tests.Base
{
    public abstract class BaseTest : IDisposable
    {
        private readonly IConfiguration _configuration;
        protected readonly FormsDbContext _context;
        protected readonly RepositoryManager _repositoryManager;

        protected BaseTest()
        {
            _configuration = TestHelpers.GetConfiguration();
            _context = TestHelpers.GetContext(_configuration);
            _repositoryManager = new RepositoryManager(_context);
        }


        protected void ExecuteInATransaction(Action actionToExecute)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using var trans = _context.Database.BeginTransaction();
                actionToExecute();
                trans.Rollback();
            });
        }

        protected void ExecuteInASharedTransaction(Action<IDbContextTransaction> actionToExecute)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using IDbContextTransaction trans = _context.Database.BeginTransaction(IsolationLevel.ReadUncommitted);
                actionToExecute(trans);
                trans.Rollback();
            });
        }
        public virtual void Dispose()
        {
            _context.Dispose();
        }
    }
}