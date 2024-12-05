using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using IW5.BL.Models.ManipulationModels.UserModels;
using IW5.Common.Enums.Sorts;
using IW5.DAL.Tests.Base;
using Xunit;

namespace IW5.API.Tests
{
    [Collection("Integration Tests")]
    public class UsersControllerTests : BaseTest, IClassFixture<EnsureIW5DatabaseTestFixture>//, IAsyncDisposable
    {
        private readonly CustomWebApplicationFactory application;
        private readonly Lazy<HttpClient> client;

        public UsersControllerTests() : base()
        {
            application = new CustomWebApplicationFactory();
            client = new Lazy<HttpClient>(application.CreateClient());
        }

        [Fact]
        public async Task GetAllUsers_Returns_ListOfUsers()
        {
            // Act
            var response = await client.Value.GetAsync("/api/Users/all");
            response.EnsureSuccessStatusCode();

            // Assert
            var users = await response.Content.ReadFromJsonAsync<List<UserListModel>>();
            users.Should().NotBeNull();
            users.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetUserById_Returns_CorrectUser()
        {
            // Arrange
            var testUserId = Guid.Parse("980745cb-b407-4b72-9a6b-1d5c9cf6a5ef"); // Assuming this user exists

            // Act
            var response = await client.Value.GetAsync($"/api/Users/{testUserId}");
            response.EnsureSuccessStatusCode();

            // Assert
            var user = await response.Content.ReadFromJsonAsync<UserDetailModel>();
            user.Should().NotBeNull();
            user.Id.Should().Be(testUserId);
        }

        [Fact]
        public async Task GetUserById_Returns_NotFound_When_UserDoesNotExist()
        {
            // Arrange
            var nonExistentUserId = Guid.NewGuid();

            // Act
            var response = await client.Value.GetAsync($"/api/Users/{nonExistentUserId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateUser_Returns_CreatedUser()
        {
            // Arrange
            var newUser = new UserForManipulationModel
            {
                Name = "Test User",
                Email = "testuser@example.com",
                Role = IW5.Common.Enums.Role.Basic,
                PhotoUrl = "https://example.com/photo.jpg",
            };

            // Act
            var response = await client.Value.PostAsJsonAsync("/api/Users", newUser);
            response.EnsureSuccessStatusCode();

            // Assert
            var createdUser = await response.Content.ReadFromJsonAsync<UserDetailModel>();
            createdUser.Should().NotBeNull();
            createdUser.Name.Should().Be(newUser.Name);
        }

        [Fact]
        public async Task UpdateUser_Returns_OK_On_SuccessfulUpdate()
        {
            // Arrange
            var testUserId = Guid.Parse("c2ad823a-c3bc-49cb-a930-2fd719c0e997"); // Assuming this user exists
            var updatedUser = await client.Value.GetAsync($"/api/Users/{testUserId}");
            var user = await updatedUser.Content.ReadFromJsonAsync<UserDetailModel>();
            user.Name = "Updated Test User";
            user.PhotoUrl = "some url";
            // Act

            var response = await client.Value.PutAsJsonAsync($"/api/Users/{testUserId}", user);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteUser_Returns_NoContent_On_SuccessfulDeletion()
        {
            // Arrange
            var testUserId = Guid.Parse("ad2d34eb-d2a8-4e0a-9a17-c0d295d8995a"); // Assuming this user exists

            // Act
            var response = await client.Value.DeleteAsync($"/api/Users/{testUserId}");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Verify deletion by attempting to fetch the user again
            var getResponse = await client.Value.GetAsync($"/api/Users/{testUserId}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetFilteredUsers_Returns_ValidData_When_Filtered()
        {
            // Arrange
            var searchString = "John"; // Assuming some users with this string in their names exist
            var filterType = UserSortType.None; // Assuming this is a valid filter type

            // Act
            var response = await client.Value.GetAsync($"/api/Users/filtered?type={filterType}&searchString={searchString}");
            response.EnsureSuccessStatusCode();

            // Assert
            var filteredUsers = await response.Content.ReadFromJsonAsync<List<UserListModel>>();
            filteredUsers.Should().NotBeNull();
            filteredUsers.Should().NotBeEmpty();
        }

        public async ValueTask DisposeAsync()
        {
            await application.DisposeAsync();
        }
    }
}
