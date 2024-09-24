using IW5.Dal.Tests.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using IW5.Dal.Initialization;
using IW5.Models.Entities;
using IW5.DAL.Contracts;
using IW5.DAL.Repository;
using IW5.Common.Enums;

namespace IW5.Dal.Tests.IntegrationTests
{
    [Collection("Integration Tests")]
    public class UserTests : BaseTest, IClassFixture<EnsureIW5DatabaseTestFixture>
    {

        protected IUserRepository _userRepository;

        public UserTests() : base()
        {
            _userRepository = RepositoryManager._userRepository.Value;
        }

        [Fact]
        public async Task ShouldGetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            Assert.Equal(6, users.Count());
        }

        [Fact]
        public async Task ShouldGetCorrectUser()
        {
            var expectedEntityId = Guid.Parse("c2ad823a-c3bc-49cb-a930-2fd719c0e997");
            var actualEntity = await _userRepository.GetById(Guid.Parse("c2ad823a-c3bc-49cb-a930-2fd719c0e997"));
            Assert.Equal(expectedEntityId, actualEntity.Id);
        }

        [Fact]
        public async Task ShouldGetUserWithForms()
        {
            var user = Guid.Parse("980745cb-b407-4b72-9a6b-1d5c9cf6a5ef");
            var actualEntity = await _userRepository.GetById(Guid.Parse("980745cb-b407-4b72-9a6b-1d5c9cf6a5ef"));
            Assert.Equal(4, actualEntity.Forms.Count);
        }

        [Fact]
        public async Task ShouldInsertNewUser()
        {
            var newUserId = Guid.NewGuid();
            var newUser = new User()
            {
                Id = newUserId,
                Name = "Mikhail Vorobev",
                PhotoUrl = "Netu",
                Email = "U nego",
                PhoneNumber = "123456789",
                Forms = new List<Form>(),
                Role = Role.Basic,
                CreatedAt = DateTime.Now

            };
            _userRepository.Create(newUser);
            await RepositoryManager.SaveAsync();

            var expectedUser = await _userRepository.GetById(newUserId);

            Assert.NotNull(expectedUser);
            Assert.Equal(newUserId, expectedUser.Id);
        }

        [Fact]
        public async Task ShouldUpdateUser()
        {
            var outdatedEntity = await _userRepository.GetById(Guid.Parse("980745cb-b407-4b72-9a6b-1d5c9cf6a5ef"));
            var updatedEntity = outdatedEntity;
            updatedEntity.PhoneNumber = "1234567890";

            _userRepository.Update(updatedEntity);
            await RepositoryManager.SaveAsync();

            var actualEntity = await _userRepository.GetById(Guid.Parse("980745cb-b407-4b72-9a6b-1d5c9cf6a5ef"));

            Assert.NotNull(actualEntity);
            Assert.Equal("1234567890", actualEntity.PhoneNumber);
        }

        [Fact]
        public async Task ShouldDeleteUser()
        {
            var deletingEntity = await _userRepository.GetById(Guid.Parse("980745cb-b407-4b72-9a6b-1d5c9cf6a5ef"));
            _userRepository.Delete(deletingEntity);
            await RepositoryManager.SaveAsync();

            var actualEntity = await _userRepository.GetById(Guid.Parse("980745cb-b407-4b72-9a6b-1d5c9cf6a5ef"));

            Assert.Null(actualEntity);
            
        }
    }
}