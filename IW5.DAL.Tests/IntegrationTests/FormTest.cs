using IW5.Common.Enums;
using IW5.DAL.Contracts;
using IW5.DAL.Repository;
using IW5.Dal.Tests.Base;
using IW5.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IW5.DAL.Tests.IntegrationTests
{
    [Collection("Integration Tests")]
    public class FormTests : BaseTest, IClassFixture<EnsureIW5DatabaseTestFixture>
    {

        private readonly IFormRepository _formRepository;

        public FormTests() : base()
        {
            _formRepository = RepositoryManager.Form;
        }

        [Fact]
        public async Task ShouldGetAllForms()
        {
            var forms = await _formRepository.GetAll(false).ToListAsync();
            Assert.Equal(14, forms.Count());
        }

        [Fact]
        public async Task ShouldGetCorrectForm()
        {
            var expectedEntityTitle = "Remote Work Feedback Survey";
            var actualEntity = await _formRepository.GetFormByTitleAsync(expectedEntityTitle, false);
            Assert.Equal(expectedEntityTitle, actualEntity.Title);
        }

        [Fact]
        public async Task ShouldGetFormWithQuestions()
        {
            var form = "Remote Work Feedback Survey";
            var actualEntity = await _formRepository.GetFormByTitleAsync(form, trackChanges: false);
            Assert.Equal(3, actualEntity.Questions.Count);
        }

        [Fact]
        public async Task ShouldInsertNewForm()
        {
            var newFormTitle = "Albert Popoy's form";
            var newForm = new Form()
            {
                Id = Guid.NewGuid(),
                AuthorId = Guid.Parse("c2ad823a-c3bc-49cb-a930-2fd719c0e997"),
                Title = newFormTitle,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                CreatedAt = DateTime.Now

            };
            _formRepository.Create(newForm);
            await RepositoryManager.SaveAsync();

            var expectedForm = await _formRepository.GetFormByTitleAsync(newFormTitle, trackChanges: false);

            Assert.NotNull(expectedForm);
            Assert.Equal(newFormTitle, expectedForm.Title);
        }

        [Fact]
        public async Task ShouldUpdateForm()
        {
            var title = "Marketing Campaign Effectiveness";
            var outdatedEntity = await _formRepository.GetFormByTitleAsync(title, trackChanges: false);
            var updatedEntity = outdatedEntity;
            var newTitle = "Haha! Changed!";
            updatedEntity.Title = newTitle;

            _formRepository.Update(updatedEntity);
            await RepositoryManager.SaveAsync();

            var actualEntity = await _formRepository.GetFormByTitleAsync(newTitle, false);

            Assert.NotNull(actualEntity);
            Assert.Equal("Haha! Changed!", actualEntity.Title);
        }

        [Fact]
        public async Task ShouldDeleteForm()
        {
            var title = "Weekly Team Retrospective";
            var deletingEntity = await _formRepository.GetFormByTitleAsync(title, false);
            _formRepository.Delete(deletingEntity);
            await RepositoryManager.SaveAsync();

            var actualEntity = await _formRepository.GetFormByTitleAsync(title, false);

            Assert.Null(actualEntity);

        }
    }
}
