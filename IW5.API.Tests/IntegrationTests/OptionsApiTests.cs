using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using IW5.BL.Models.ListModels;
using IW5.BL.Models.ManipulationModels.OptionModels;
using IW5.DAL.Initialization;
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
            var response = await client.Value.GetAsync("/api/Options/all");
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
            var testOptionId = Guid.Parse("fcd24794-b03f-4e93-bfe6-896981ed87f7"); 

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
        [Fact]
        public async Task CreateOption_Returns_CreatedOption()
        {
            // Arrange
            var newOption = new OptionForManipulationDTO
            {
                Text = "Test Option",
                QuestionId = SampleData.Questions.First().Id,
                IsCheked = false
            };

            // Act
            var response = await client.Value.PostAsJsonAsync("/api/Options", newOption);
            response.EnsureSuccessStatusCode();

            // Assert
            var createdOption = await response.Content.ReadFromJsonAsync<OptionListModel>();
            createdOption.Should().NotBeNull();
            createdOption.Text.Should().Be(newOption.Text);
        }
        [Fact]
        public async Task CreateOption_Returns_BadRequest_When_DataIsInvalid()
        {
            // Arrange
            OptionForManipulationDTO invalidOption = null;

            // Act
            var response = await client.Value.PostAsJsonAsync("/api/Options", invalidOption);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task UpdateOption_Returns_Ok()
        {
            // Arrange
            var existingOptionId = Guid.Parse("fcd24794-b03f-4e93-bfe6-896981ed87f7");
            var updatedOption = new OptionForManipulationDTO
            {
                Text = "Updated Option",
                QuestionId = SampleData.Questions.First().Id,
                IsCheked = true
            };

            // Act
            var response = await client.Value.PutAsJsonAsync($"/api/Options/{existingOptionId}", updatedOption);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        [Fact]
        public async Task UpdateOption_Returns_NotFound_When_OptionDoesNotExist()
        {
            // Arrange
            var nonExistentOptionId = Guid.NewGuid();
            var updatedOption = new OptionForManipulationDTO
            {
                Text = "Non-existent Option",
                IsCheked = false
            };

            // Act
            var response = await client.Value.PutAsJsonAsync($"/api/Options/{nonExistentOptionId}", updatedOption);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
        [Fact]
        public async Task DeleteOption_Returns_NoContent()
        {
            // Arrange
            var existingOptionId = Guid.Parse("f5f1f7b0-1e65-42e1-b9b3-01efc54e1de3");

            // Act
            var response = await client.Value.DeleteAsync($"/api/Options/{existingOptionId}");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }
        [Fact]
        public async Task DeleteOption_Returns_NotFound_When_OptionDoesNotExist()
        {
            // Arrange
            var nonExistentOptionId = Guid.NewGuid();

            // Act
            var response = await client.Value.DeleteAsync($"/api/Options/{nonExistentOptionId}");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }


        public async ValueTask DisposeAsync()
        {
            await application.DisposeAsync();
        }
    }
}
