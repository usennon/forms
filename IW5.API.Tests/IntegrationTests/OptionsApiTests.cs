using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using IW5.BL.Models.ListModels;
using IW5.DAL.Tests.Base;
using Xunit;

namespace IW5.API.Tests
{
    [Collection("Integration Tests")]
    public class OptionsControllerTests : BaseTest, IClassFixture<EnsureIW5DatabaseTestFixture>//, IAsyncDisposable
    {
        private readonly CustomWebApplicationFactory application;
        private readonly Lazy<HttpClient> client;

        public OptionsControllerTests() : base()
        {
            application = new CustomWebApplicationFactory();
            client = new Lazy<HttpClient>(application.CreateClient());
        }

        [Fact]
        public async Task GetAllOptions_Returns_At_Least_One_Option()
        {
            // Act
            var response = await client.Value.GetAsync("/api/Options");
            response.EnsureSuccessStatusCode();

            // Assert
            var options = await response.Content.ReadFromJsonAsync<ICollection<OptionListModel>>();
            options.Should().NotBeNull();
            options.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetOptionById_Returns_Correct_Option()
        {
            // Arrange
            var testOptionId = Guid.Parse("f5f1f7b0-1e65-42e1-b9b3-01efc54e1de3"); 

            // Act
            var response = await client.Value.GetAsync($"/api/Options/{testOptionId}");
            response.EnsureSuccessStatusCode();

            // Assert
            var option = await response.Content.ReadFromJsonAsync<OptionListModel>();
            option.Should().NotBeNull();
            option.Id.Should().Be(testOptionId);
        }

        [Fact]
        public async Task GetOptionById_Returns_NotFound_When_OptionDoesNotExist()
        {
            // Arrange
            var nonExistentOptionId = Guid.NewGuid();

            // Act
            var response = await client.Value.GetAsync($"/api/Options/{nonExistentOptionId}");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        public async ValueTask DisposeAsync()
        {
            await application.DisposeAsync();
        }
    }
}
