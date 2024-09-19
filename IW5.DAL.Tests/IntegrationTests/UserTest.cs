using IW5.Dal.Tests.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using IW5.Models.Entities;

namespace IW5.Dal.Tests.IntegrationTests
{
    [Collection("Integration Tests")]
    public class CarTests : BaseTest, IClassFixture<EnsureAutoLotDatabaseTestFixture>
    {
        

        [Fact]
        public void ShouldGetAllUsers()
        {
            IQueryable<User> query = Context.Users.IgnoreQueryFilters();
            var qs = query.ToQueryString();
            var users = query.ToList();
            Assert.Equal(2, users.Count);
        }
    }
}