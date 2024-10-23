using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using IW5.DAL;
using IW5.DAL.Repository;
using IW5.BL.Tests;
using IW5.BL.Tests.BLTests;

namespace IW5.BL.Tests.Base
{
    public abstract class BaseTest : IClassFixture<EnsureIW5DatabaseTestFixture>
    {

        protected readonly FormsDbContext _context;
        protected readonly RepositoryManager _repositoryManager;

        protected BaseTest(EnsureIW5DatabaseTestFixture fixture)
        {
            _context = fixture.GetContext();
            _repositoryManager = new RepositoryManager(_context);
        }



        protected async Task ExecuteInATransactionAsync(Func<Task> actionToExecute)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var trans = await _context.Database.BeginTransactionAsync();
                await actionToExecute();
                await trans.RollbackAsync(); 
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
    }
}