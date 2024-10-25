using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ManipulationModels.QuestionModels;
using IW5.Common.Enums.Sorts;
using IW5.DAL.Initialization;
using IW5.DAL.Tests.Base;
using Xunit;

namespace IW5.API.Tests
{
    [Collection("Integration Tests")]
    public class QuestionsControllerTests : BaseTest, IClassFixture<EnsureIW5DatabaseTestFixture>// IAsyncDisposable
    {
        private readonly CustomWebApplicationFactory application;
        private readonly Lazy<HttpClient> client;

        public QuestionsControllerTests() : base()
        {
            application = new CustomWebApplicationFactory();
            client = new Lazy<HttpClient>(application.CreateClient());
        }

        [Fact]
        public async Task GetAllQuestions_Returns_ListOfQuestions()
        {
            // Act
            var response = await client.Value.GetAsync("/api/Questions/all");
            response.EnsureSuccessStatusCode();

            // Assert
            var questions = await response.Content.ReadFromJsonAsync<List<QuestionDetailModel>>();
            questions.Should().NotBeNull();
            questions.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetQuestionById_Returns_CorrectQuestion()
        {
            // Arrange
            var testQuestionId = Guid.Parse("5bc27217-6817-40e4-b8d1-60dc9aca3e83"); // Assuming this question exists

            // Act
            var response = await client.Value.GetAsync($"/api/Questions/{testQuestionId}");
            response.EnsureSuccessStatusCode();

            // Assert
            var question = await response.Content.ReadFromJsonAsync<QuestionDetailModel>();
            question.Should().NotBeNull();
            question.Id.Should().Be(testQuestionId);
        }

        [Fact]
        public async Task GetQuestionById_Returns_NotFound_When_QuestionDoesNotExist()
        {
            // Arrange
            var nonExistentQuestionId = Guid.NewGuid();

            // Act
            var response = await client.Value.GetAsync($"/api/Questions/{nonExistentQuestionId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateQuestion_Returns_CreatedQuestion()
        {
            // Arrange
            var newQuestion = new QuestionForManipulationDTO
            {
                Description = "Test question description",
                IsRequired = true,
                FormId = SampleData.Forms.First().Id,
                Text = "Test question text"
            };

            // Act
            var response = await client.Value.PostAsJsonAsync("/api/Questions", newQuestion);
            response.EnsureSuccessStatusCode();

            // Assert
            var createdQuestion = await response.Content.ReadFromJsonAsync<QuestionDetailModel>();
            createdQuestion.Should().NotBeNull();
            createdQuestion.Text.Should().Be(newQuestion.Text);
        }

        [Fact]
        public async Task UpdateQuestion_Returns_OK_On_SuccessfulUpdate()
        {
            // Arrange
            var testQuestionId = Guid.Parse("5bc27217-6817-40e4-b8d1-60dc9aca3e83"); // Assuming this question exists
            var updatedQuestion = await client.Value.GetAsync($"/api/Questions/{testQuestionId}");
            var question = await updatedQuestion.Content.ReadFromJsonAsync<QuestionDetailModel>();
            question!.Text = "Updated Test Question";
            question!.Description = "Some descripton here";


            // Act
            var response = await client.Value.PutAsJsonAsync($"/api/Questions/{testQuestionId}", question);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task DeleteQuestion_Returns_NoContent_On_SuccessfulDeletion()
        {
            // Arrange
            var testQuestionId = Guid.Parse("59fe555e-3bcc-4ace-b9fc-68b76805ac59"); // Assuming this question exists

            // Act
            var response = await client.Value.DeleteAsync($"/api/Questions/{testQuestionId}");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Verify deletion by attempting to fetch the question again
            var getResponse = await client.Value.GetAsync($"/api/Questions/{testQuestionId}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetFilteredQuestions_Returns_ValidData_When_Filtered()
        {
            // Arrange
            var searchString = "satisfied"; // Assuming some questions with this string in their text exist
            var sortType = QuestionSortType.None; // Assuming 1 is some valid sort type

            // Act
            var response = await client.Value.GetAsync($"/api/Questions/filtered?searchString={searchString}&type={sortType}");
            response.EnsureSuccessStatusCode();

            // Assert
            var filteredQuestions = await response.Content.ReadFromJsonAsync<List<QuestionDetailModel>>();
            filteredQuestions.Should().NotBeNull();
            filteredQuestions.Should().NotBeEmpty();
        }

        public async ValueTask DisposeAsync()
        {
            await application.DisposeAsync();
        }
    }
}
