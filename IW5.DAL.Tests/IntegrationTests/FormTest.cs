using IW5.Common.Enums;
using IW5.DAL.Contracts;
using IW5.DAL.Repository;
using IW5.Dal.Tests.Base;
using IW5.Models.Entities;

namespace IW5.DAL.Tests.IntegrationTests
{
    [Collection("Integration Tests")]
    public class FormTests : BaseTest, IClassFixture<EnsureIW5DatabaseTestFixture>
    {

        protected IFormRepository _formRepository;

        public FormTests() : base()
        {
            _formRepository = RepositoryManager._formRepository.Value;
        }

        [Fact]
        public async Task ShouldGetAllForms()
        {
            var forms = await _formRepository.GetAllFormsAsync(false);
            Assert.Equal(14, forms.Count());
        }

        [Fact]
        public async Task ShouldGetCorrectForm()
        {
            var expectedEntityId = Guid.Parse("6f28e7f7-42b2-4d98-a6ae-2fe7d7d6fd6c");
            var actualEntity = await _formRepository.GetFormByIdAsync(expectedEntityId, false);
            Assert.Equal(expectedEntityId, actualEntity.Id);
        }

        [Fact]
        public async Task ShouldGetFormWithQuestions()
        {
            var form = Guid.Parse("6f28e7f7-42b2-4d98-a6ae-2fe7d7d6fd6c");
            var actualEntity = await _formRepository.GetFormByIdAsync(form, trackChanges: false);
            Assert.Equal(3, actualEntity.Questions.Count);
        }

        [Fact]
        public async Task ShouldInsertNewForm()
        {
            var newFormId = Guid.NewGuid();
            var newForm = new Form()
            {
                Id = newFormId,
                Title = "Albert Popoy",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                CreatedAt = DateTime.Now

            };
            _formRepository.CreateFormForAuthor(Guid.Parse("c2ad823a-c3bc-49cb-a930-2fd719c0e997"), newForm);
            await RepositoryManager.SaveAsync();

            var expectedForm = await _formRepository.GetFormByIdAsync(newFormId, trackChanges: false);

            Assert.NotNull(expectedForm);
            Assert.Equal(newFormId, expectedForm.Id);
        }

        [Fact]
        public async Task ShouldUpdateForm()
        {
            var id = Guid.Parse("df1c5c1d-7a55-4b4a-8e2e-bd3f31230f71");
            var outdatedEntity = await _formRepository.GetFormByIdAsync(id, trackChanges: false);
            var updatedEntity = outdatedEntity;
            updatedEntity.Title = "Haha! Changed!";

            _formRepository.Update(updatedEntity);
            await RepositoryManager.SaveAsync();

            var actualEntity = await _formRepository.GetFormByIdAsync(id, false);

            Assert.NotNull(actualEntity);
            Assert.Equal("Haha! Changed!", actualEntity.Title);
        }

        [Fact]
        public async Task ShouldDeleteUser()
        {
            var id = Guid.Parse("982f69fd-6278-4e5d-b896-97e1be5b17d2");
            var deletingEntity = await _formRepository.GetFormByIdAsync(id, false);
            _formRepository.DeleteForm(deletingEntity);
            await RepositoryManager.SaveAsync();

            var actualEntity = await _formRepository.GetFormByIdAsync(id, false);

            Assert.Null(actualEntity);

        }
    }
}
