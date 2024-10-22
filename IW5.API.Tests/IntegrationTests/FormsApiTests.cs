using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using Xunit;

namespace IW5.API.Tests
{
    public class FormsControllerTests : IAsyncDisposable
    {
        private readonly CustomWebApplicationFactory application;
        private readonly Lazy<HttpClient> client;

        public FormsControllerTests()
        {
            application = new CustomWebApplicationFactory();
            client = new Lazy<HttpClient>(application.CreateClient());
        }

        [Fact]
        public async Task GetAllForms_Returns_ListOfForms()
        {
            // Act
            var response = await client.Value.GetAsync("/api/Forms/all");
            response.EnsureSuccessStatusCode();

            // Assert
            var forms = await response.Content.ReadFromJsonAsync<List<FormListModel>>();
            forms.Should().NotBeNull();
            forms.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetFormById_Returns_CorrectForm()
        {
            // Arrange
            var testFormId = Guid.Parse("df1c5c1d-7a55-4b4a-8e2e-bd3f31230f71"); // Assuming this form exists

            // Act
            var response = await client.Value.GetAsync($"/api/Forms/{testFormId}");
            response.EnsureSuccessStatusCode();

            // Assert
            var form = await response.Content.ReadFromJsonAsync<FormDetailModel>();
            form.Should().NotBeNull();
            form.Id.Should().Be(testFormId);
        }

        [Fact]
        public async Task GetFormById_Returns_NotFound_When_FormDoesNotExist()
        {
            // Arrange
            var nonExistentFormId = Guid.NewGuid();

            // Act
            var response = await client.Value.GetAsync($"/api/Forms/{nonExistentFormId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateForm_Returns_CreatedForm()
        {
            // Arrange
            var newForm = new FormDetailModel
            {
                Id = Guid.NewGuid(),
                AuthorId = Guid.Parse("980745cb-b407-4b72-9a6b-1d5c9cf6a5ef"),
                Title = "New Test Form",
                AuthorName = "Test Author",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddHours(1),
                CreatedAt = DateTime.UtcNow
               // Questions = new List<QuestionListModel>()
            };

            // Act
            var response = await client.Value.PostAsJsonAsync("/api/Forms", newForm);
            response.EnsureSuccessStatusCode();

            // Assert
            var createdForm = await response.Content.ReadFromJsonAsync<FormDetailModel>();
            createdForm.Should().NotBeNull();
            createdForm.Title.Should().Be(newForm.Title);
        }

        [Fact]
        public async Task UpdateForm_Returns_OK_On_SuccessfulUpdate()
        {
            // Arrange
            var testFormId = Guid.Parse("34a2a6b7-84e6-4ffb-9cd7-f506e7f853b7"); // Assuming this form exists
            var updatedForm = await client.Value.GetAsync($"/api/Forms/{testFormId}");
            var form =  await updatedForm.Content.ReadFromJsonAsync<FormDetailModel>();
            form.Title = "Updated Test Form";
            form.AuthorName = "Updated Author";
            // Act

            var response = await client.Value.PutAsJsonAsync($"/api/Forms/{testFormId}", form);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteForm_Returns_OK_On_SuccessfulDeletion()
        {
            // Arrange
            var testFormId = Guid.Parse("34a2a6b7-84e6-4ffb-9cd7-f506e7f853b7"); // Assuming this form exists

            // Act
            var response = await client.Value.DeleteAsync($"/api/Forms/{testFormId}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Verify deletion by attempting to fetch the form again
            var getResponse = await client.Value.GetAsync($"/api/Forms/{testFormId}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetFilteredForms_Returns_ValidData_When_Filtered()
        {
            // Arrange
            var searchString = "Employee"; // Assuming some forms with this string in their titles exist
            var filterType = 1; // Assuming 1 is some valid filter type

            // Act
            var response = await client.Value.GetAsync($"/api/Forms/filter/{filterType}/{searchString}");
            response.EnsureSuccessStatusCode();

            // Assert
            var filteredForms = await response.Content.ReadFromJsonAsync<List<FormListModel>>();
            filteredForms.Should().NotBeNull();
            filteredForms.Should().NotBeEmpty();
        }

        public async ValueTask DisposeAsync()
        {
            await application.DisposeAsync();
        }
    }
}
