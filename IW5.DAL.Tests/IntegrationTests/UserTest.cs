using IW5.DAL.Tests.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using IW5.DAL.Initialization;
using IW5.Models.Entities;
using IW5.DAL.Contracts;
using IW5.DAL.Repository;
using IW5.Common.Enums;
using System.ComponentModel.Design;

namespace IW5.DAL.Tests.IntegrationTests
{
    [Collection("Integration Tests")]
    public class UserTests : BaseTest, IClassFixture<EnsureIW5DatabaseTestFixture>
    {

        private readonly IUserRepository _userRepository;

        public UserTests() : base()
        {
            _userRepository = RepositoryManager.User;
        }

        [Fact]
        public async Task ShouldGetAllUsers()
        {
            var users = await _userRepository.GetAll(false).ToListAsync();
            Assert.Equal(6, users.Count());
        }

        [Fact]
        public async Task ShouldGetCorrectUser()
        {
            var expectedEntityId = Guid.Parse("c2ad823a-c3bc-49cb-a930-2fd719c0e997");
            var actualEntity = await _userRepository.GetByIdAsync(expectedEntityId, false);
            Assert.Equal(expectedEntityId, actualEntity.Id);
        }

        [Fact]
        public async Task ShouldGetUserWithForms()
        {
            var user = "John Doe";
            var actualEntity = await _userRepository.GetUserByNameAsync(user, trackChanges: false);
            Assert.Equal(4, actualEntity.Forms.Count);
        }

        [Fact]
        public async Task ShouldInsertNewUser()
        {
            var name = "Mikhail Vorobev";
            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                UserName = name,
                PhotoUrl = "Netu",
                Email = "U nego",
                PhoneNumber = "123456789",
                Forms = new List<Form>(),
                Role = Role.Basic,
                CreatedAt = DateTime.Now

            };
            _userRepository.Create(newUser);
            await RepositoryManager.SaveAsync();

            var expectedUser = await _userRepository.GetUserByNameAsync(name, trackChanges: false);

            Assert.NotNull(expectedUser);
            Assert.Equal(name, expectedUser.UserName);
        }

        [Fact]
        public async Task ShouldUpdateUser()
        {
            var outdatedEntity = await _userRepository.GetByIdAsync(Guid.Parse("980745cb-b407-4b72-9a6b-1d5c9cf6a5ef"), trackChanges: false);
            var updatedEntity = outdatedEntity;
            updatedEntity.PhoneNumber = "1234567890";

            await _userRepository.UpdateAsync(updatedEntity);
            await RepositoryManager.SaveAsync();

            var actualEntity = await _userRepository.GetByIdAsync(Guid.Parse("980745cb-b407-4b72-9a6b-1d5c9cf6a5ef"), false);

            Assert.NotNull(actualEntity);
            Assert.Equal("1234567890", actualEntity.PhoneNumber);
        }

        [Fact]
        public async Task ShouldDeleteUser()
        {
            var deletingEntity = await _userRepository.GetByIdAsync(Guid.Parse("980745cb-b407-4b72-9a6b-1d5c9cf6a5ef"), false);
            _userRepository.Delete(deletingEntity);
            await RepositoryManager.SaveAsync();

            var actualEntity = await _userRepository.GetByIdAsync(Guid.Parse("980745cb-b407-4b72-9a6b-1d5c9cf6a5ef"), false);

            Assert.Null(actualEntity);

        }
    }
}