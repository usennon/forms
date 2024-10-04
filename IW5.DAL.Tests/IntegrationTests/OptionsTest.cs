using IW5.Common.Enums;
using IW5.Dal.Initialization;
using IW5.Dal.Tests.Base;
using IW5.DAL.Contracts;
using IW5.DAL.Repository;
using IW5.Models.Entities;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace IW5.DAL.Tests.IntegrationTests
{
    [Collection("Integration Tests")]
    public class OptionsTests : BaseTest, IClassFixture<EnsureIW5DatabaseTestFixture>
    {
        private readonly IOptionRepository _optionRepository;

        public OptionsTests () : base() 
        {
            _optionRepository = RepositoryManager.Option;
        }

        [Fact]
        public async Task ShouldGetAllOptions()
        {
            var options = await _optionRepository.GetAllOptionsAsync(false);
            Assert.Equal(12, options.Count());
        }
        [Fact]
        public async Task ShouldGetAllOptionsFromQuestion()
        {
            var questionId = Guid.Parse("5bc27217-6817-40e4-b8d1-60dc9aca3e83");
            var options = await _optionRepository.GetOptionsFromQuestionAsync(questionId, false);

            var expectedIds = SampleData.Options
                .Where(n => n.QuestionId == questionId)
                .Select(n => n.Id).OrderBy(id => id)
                .ToList();

            var actualIds = options
                .Select(n => n.Id)
                .OrderBy(id => id)
                .ToList();

            

            Assert.Equal(5, options.Count());
            Assert.Equal(expectedIds, actualIds);
        }
        [Fact]
        public async Task ShouldGetSpecificOption()
        {
            var expectedId = SampleData.Options[5].Id;
            var option = await _optionRepository.GetOptionByIdAsync(expectedId, false);

            Assert.NotNull(option);
            Assert.Equal(option.Id, expectedId);
        }
        [Fact]
        public async Task ShouldInsertNewOption()
        {
            var questionId = Guid.Parse("fd9b87c1-b4e6-4c8f-a8f5-d78c9a1f2a3c");
            var optionId1 = Guid.NewGuid();
            var optionId2 = Guid.NewGuid();
            var optionId3 = Guid.NewGuid();
            var newOptions = new List<Option>()
            {   
                new Option
                {
                Id = optionId1,
                Text = "Yes",
                CreatedAt = DateTime.Now
                },
                new Option
                {
                Id = optionId2,
                Text = "Definitely",
                CreatedAt = DateTime.Now
                },
                new Option
                {
                Id = optionId3,
                Text = "Absolutely!!!",
                CreatedAt = DateTime.Now
                }
            };
            _optionRepository.CreateListOfOptionsForQuestion(questionId, newOptions);
            await RepositoryManager.SaveAsync();

            var expectedQuestions = await _optionRepository.GetOptionsFromQuestionAsync(questionId, false);

            Assert.NotNull(expectedQuestions);
            Assert.Equal(3, expectedQuestions.Count());
        }
        [Fact]
        public async Task ShouldUpdateOption()
        {
            var optionId = Guid.Parse("a1c0f4ab-6c4d-4b82-baf2-123c78e50d3a");
            var outdatedEntity = await _optionRepository.GetOptionByIdAsync(optionId, false);
            var updatedEntity = outdatedEntity;

            updatedEntity.Text = "Maybe!";
            updatedEntity.CreatedAt = DateTime.Now;

            _optionRepository.Update(updatedEntity);
            await RepositoryManager.SaveAsync();

            var expectedEntity = await _optionRepository.GetOptionByIdAsync(optionId, false);
            Assert.NotNull(expectedEntity);
            Assert.Equal(updatedEntity.Text, expectedEntity.Text);
        }

        [Fact]
        public async Task ShouldDeleteOption()
        {
            var optionId = Guid.Parse("5f967174-7366-409e-b33e-6b2d4bce532a");
            var deletingEntity = await _optionRepository.GetOptionByIdAsync(optionId, false);
            _optionRepository.DeleteOption(deletingEntity);
            await RepositoryManager.SaveAsync();

            var actualEntity = await _optionRepository.GetOptionByIdAsync(optionId, false);

            Assert.Null(actualEntity);

        }

    }
}
