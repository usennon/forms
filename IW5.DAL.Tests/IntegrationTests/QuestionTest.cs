using IW5.DAL.Contracts;
using IW5.Dal.Tests.Base;
using IW5.Models.Entities;
using IW5.Common.Enums;

namespace IW5.DAL.Tests.IntegrationTests
{
    [Collection("Integration Tests")]
    public class QuestionTests : BaseTest, IClassFixture<EnsureIW5DatabaseTestFixture>
    {

        private readonly IQuestionRepository _questionRepository;

        public QuestionTests() : base()
        {
            _questionRepository = RepositoryManager.Question;
        }

        [Fact]
        public async Task ShouldGetAllQuestions()
        {
            var forms = await _questionRepository.GetAllQuestionsAsync(false);
            Assert.Equal(23, forms.Count());
        }

        [Fact]
        public async Task ShouldGetCorrectQuestion()
        {
            var expectedEntityId = Guid.Parse("5bc27217-6817-40e4-b8d1-60dc9aca3e83");
            var actualEntity = await _questionRepository.GetQuestionByIdAsync(expectedEntityId, false);
            Assert.Equal(expectedEntityId, actualEntity.Id);
        }

        [Fact]
        public async Task ShouldGetQuestionWithOptions()
        {
            var question = Guid.Parse("5bc27217-6817-40e4-b8d1-60dc9aca3e83");
            var actualEntity = await _questionRepository.GetQuestionByIdAsync(question, trackChanges: false);
            Assert.Equal(5, actualEntity?.Options?.Count);
        }

        [Fact]
        public async Task ShouldInsertNewQuestion()
        {
            var newQuestionId = Guid.NewGuid();
            var newQuestion = new Question()
            {
                Id = newQuestionId,
                Text = "Why?",
                Type = QuestionType.YesNo,

                CreatedAt = DateTime.Now

            };
            _questionRepository.CreateQuestionForForm(Guid.Parse("34a2a6b7-84e6-4ffb-9cd7-f506e7f853b7"), newQuestion);
            await RepositoryManager.SaveAsync();

            var expectedForm = await _questionRepository.GetQuestionByIdAsync(newQuestionId, trackChanges: false);

            Assert.NotNull(expectedForm);
            Assert.Equal(newQuestionId, expectedForm.Id);
        }

        [Fact]
        public async Task ShouldUpdateQuestion()
        {
            var id = Guid.Parse("5bc27217-6817-40e4-b8d1-60dc9aca3e83");
            var outdatedEntity = await _questionRepository.GetQuestionByIdAsync(id, trackChanges: false);
            var updatedEntity = outdatedEntity;
            updatedEntity.Text = "Haha! Changed!";

            _questionRepository.Update(updatedEntity);
            await RepositoryManager.SaveAsync();

            var actualEntity = await _questionRepository.GetQuestionByIdAsync(id, false);

            Assert.NotNull(actualEntity);
            Assert.Equal("Haha! Changed!", actualEntity.Text);
        }

        [Fact]
        public async Task ShouldDeleteQuestion()
        {
            var id = Guid.Parse("893fe128-c0fc-4c97-9b35-745e42d0b56a");
            var deletingEntity = await _questionRepository.GetQuestionByIdAsync(id, false);
            _questionRepository.DeleteQuestion(deletingEntity);
            await RepositoryManager.SaveAsync();

            var actualEntity = await _questionRepository.GetQuestionByIdAsync(id, false);

            Assert.Null(actualEntity);

        }
    }
}
