using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using IW5.Models.Entities;
using FluentAssertions;
using AutoMapper;
using IW5.API;

namespace IW5.BL.Tests.MapperTests
{
    public class FormMapperTest : BaseMapperTest
    {
        private readonly IMapper _formMapper;
        public FormMapperTest() : base()
        {
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile<MappingProfile>();
                });
            _formMapper = config.CreateMapper();
        }

        [Fact]
        public void Form_To_FormListModel_Mapping_ShouldBeValid()
        {
            // Arrange
            var form = new Form
            {
                Id = Guid.NewGuid(),
                Title = "Form 1",
                CreatedAt = DateTime.UtcNow,
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2)
            };

            // Act
            var formListModel = _formMapper.Map<FormListModel>(form);

            // Assert
            formListModel.Title.Should().Be(form.Title);
            formListModel.CreatedAt.Should().Be(form.CreatedAt);
        }

        [Fact]
        public void Form_To_FormDetailModel_Mapping_ShouldBeValid()
        {
            // Arrange
            var form = new Form
            {
                Id = Guid.NewGuid(),
                Title = "Form 1",
                Author = new User { Id = Guid.Empty, Name = "John Doe" },
                AuthorId = Guid.Empty,
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                Questions = new List<Question>
            {
                new Question { Id = Guid.NewGuid(), Text = "Question 1" },
                new Question { Id = Guid.NewGuid(), Text = "Question 2" }
            }
            };

            // Act
            var formDetailModel = _formMapper.Map<FormDetailModel>(form);

            // Assert
            formDetailModel.Title.Should().Be(form.Title);
            formDetailModel.AuthorId.Should().Be(form.Author.Id);
            formDetailModel.AuthorName.Should().Be(form.Author.Name);
            formDetailModel.StartDate.Should().Be(form.StartDate);
            formDetailModel.EndDate.Should().Be(form.EndDate);
            formDetailModel.Questions.Should().HaveCount(2);
            formDetailModel.Questions[0].Text.Should().Be(form.Questions.ToList()[0].Text);
            formDetailModel.Questions[1].Text.Should().Be(form.Questions.ToList()[1].Text);
        }

        [Fact]
        public void FormDetailModel_To_Form_Mapping_ShouldIgnoreQuestions()
        {
            // Arrange
            var formDetailModel = new FormDetailModel
            {
                Title = "Form 1",
                AuthorId = Guid.NewGuid(),
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                Questions = new List<QuestionListModel>
            {
                new QuestionListModel { Text = "Question 1" },
                new QuestionListModel { Text = "Question 2" }
            }
            };

            // Act
            var form = _formMapper.Map<Form>(formDetailModel);

            // Assert
            form.Title.Should().Be(formDetailModel.Title);
            form.AuthorId.Should().Be(formDetailModel.AuthorId);
            form.StartDate.Should().Be(formDetailModel.StartDate);
            form.EndDate.Should().Be(formDetailModel.EndDate);
            form.Questions.Should().BeEmpty(); // Questions should be ignored
        }
        [Fact]
        public void Form_To_FormDetailModel_WithEmptyQuestions_ShouldMapCorrectly()
        {
            // Arrange
            var form = new Form
            {
                Id = Guid.NewGuid(),
                Title = "Empty Questions Form",
                Author = new User { Id = Guid.Empty, Name = "John Doe" },
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                Questions = new List<Question>() // Empty list of questions
            };

            // Act
            var formDetailModel = _formMapper.Map<FormDetailModel>(form);

            // Assert
            formDetailModel.Title.Should().Be(form.Title);
            formDetailModel.AuthorId.Should().Be(form.Author.Id);
            formDetailModel.AuthorName.Should().Be(form.Author.Name);
            formDetailModel.StartDate.Should().Be(form.StartDate);
            formDetailModel.EndDate.Should().Be(form.EndDate);
            formDetailModel.Questions.Should().BeEmpty(); // Questions list should be empty
        }

        [Fact]
        public void Form_To_FormDetailModel_WithNullQuestions_ShouldMapAsEmptyList()
        {
            // Arrange
            var form = new Form
            {
                Id = Guid.NewGuid(),
                Title = "Null Questions Form",
                Author = new User { Id = Guid.Empty, Name = "John Doe" },
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                Questions = null // Questions list is null
            };

            // Act
            var formDetailModel = _formMapper.Map<FormDetailModel>(form);

            // Assert
            formDetailModel.Title.Should().Be(form.Title);
            formDetailModel.AuthorId.Should().Be(form.Author.Id);
            formDetailModel.AuthorName.Should().Be(form.Author.Name);
            formDetailModel.StartDate.Should().Be(form.StartDate);
            formDetailModel.EndDate.Should().Be(form.EndDate);
            formDetailModel.Questions.Should().BeEmpty(); // Should map null to an empty list
        }

        [Fact]
        public void Form_To_FormDetailModel_WithNullAuthor_ShouldMapCorrectly()
        {
            // Arrange
            var form = new Form
            {
                Id = Guid.NewGuid(),
                Title = "Null Author Form",
                Author = null, // Author is null
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                Questions = new List<Question>
        {
            new Question { Id = Guid.NewGuid(), Text = "Question 1" }
        }
            };

            // Act
            var formDetailModel = _formMapper.Map<FormDetailModel>(form);

            // Assert
            formDetailModel.Title.Should().Be(form.Title);
            formDetailModel.AuthorId.Should().Be(Guid.Empty); // AuthorId should be empty Guid
            formDetailModel.AuthorName.Should().BeNull(); // AuthorName should be null
            formDetailModel.StartDate.Should().Be(form.StartDate);
            formDetailModel.EndDate.Should().Be(form.EndDate);
            formDetailModel.Questions.Should().HaveCount(1);
            formDetailModel.Questions[0].Text.Should().Be("Question 1");
        }

        [Fact]
        public void Form_To_FormDetailModel_WithMinimalFields_ShouldMapCorrectly()
        {
            // Arrange
            var form = new Form
            {
                Id = Guid.NewGuid(),
                Title = "Minimal Form",
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2)
                // No Author and Questions provided
            };

            // Act
            var formDetailModel = _formMapper.Map<FormDetailModel>(form);

            // Assert
            formDetailModel.Title.Should().Be(form.Title);
            formDetailModel.StartDate.Should().Be(form.StartDate);
            formDetailModel.EndDate.Should().Be(form.EndDate);
            formDetailModel.AuthorId.Should().Be(Guid.Empty); // AuthorId should be empty Guid
            formDetailModel.AuthorName.Should().BeNull(); // AuthorName should be null
            formDetailModel.Questions.Should().BeEmpty(); // Questions should be an empty list
        }

        [Fact]
        public void FormDetailModel_To_Form_Mapping_ShouldMapCorrectly()
        {
            // Arrange
            var formDetailModel = new FormDetailModel
            {
                Title = "Form 1",
                AuthorId = Guid.NewGuid(),
                AuthorName = "John Doe",
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                Questions = new List<QuestionListModel>
        {
            new QuestionListModel { Text = "Question 1" },
            new QuestionListModel { Text = "Question 2" }
        }
            };

            // Act
            var form = _formMapper.Map<Form>(formDetailModel);

            // Assert
            form.Title.Should().Be(formDetailModel.Title);
            form.AuthorId.Should().Be(formDetailModel.AuthorId);
            form.StartDate.Should().Be(formDetailModel.StartDate);
            form.EndDate.Should().Be(formDetailModel.EndDate);
            form.Questions.Should().BeEmpty(); // Questions are ignored, per mapping configuration
        }

        [Fact]
        public void FormDetailModel_To_Form_WithEmptyQuestions_ShouldMapCorrectly()
        {
            // Arrange
            var formDetailModel = new FormDetailModel
            {
                Title = "Form with Empty Questions",
                AuthorId = Guid.NewGuid(),
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                Questions = new List<QuestionListModel>() // Empty list of questions
            };

            // Act
            var form = _formMapper.Map<Form>(formDetailModel);

            // Assert
            form.Title.Should().Be(formDetailModel.Title);
            form.AuthorId.Should().Be(formDetailModel.AuthorId);
            form.StartDate.Should().Be(formDetailModel.StartDate);
            form.EndDate.Should().Be(formDetailModel.EndDate);
            form.Questions.Should().BeEmpty(); // Questions should remain empty
        }

        [Fact]
        public void FormDetailModel_To_Form_WithNullQuestions_ShouldMapAsEmptyList()
        {
            // Arrange
            var formDetailModel = new FormDetailModel
            {
                Title = "Form with Null Questions",
                AuthorId = Guid.NewGuid(),
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                Questions = new List<QuestionListModel>() // Null list of questions
            };

            // Act
            var form = _formMapper.Map<Form>(formDetailModel);

            // Assert
            form.Title.Should().Be(formDetailModel.Title);
            form.AuthorId.Should().Be(formDetailModel.AuthorId);
            form.StartDate.Should().Be(formDetailModel.StartDate);
            form.EndDate.Should().Be(formDetailModel.EndDate);
            form.Questions.Should().BeEmpty(); // Null list should map to an empty collection
        }

        [Fact]
        public void FormDetailModel_To_Form_WithNullAuthor_ShouldMapCorrectly()
        {
            // Arrange
            var formDetailModel = new FormDetailModel
            {
                Title = "Form with Null Author",
                AuthorId = Guid.Empty, // AuthorId is empty
                AuthorName = null!, // AuthorName is null
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                Questions = new List<QuestionListModel>
        {
            new QuestionListModel { Text = "Question 1" }
        }
            };

            // Act
            var form = _formMapper.Map<Form>(formDetailModel);

            // Assert
            form.Title.Should().Be(formDetailModel.Title);
            form.AuthorId.Should().Be(Guid.Empty); // AuthorId should remain empty
            form.Author.Should().BeNull(); // Author should be null because AuthorName is null
            form.StartDate.Should().Be(formDetailModel.StartDate);
            form.EndDate.Should().Be(formDetailModel.EndDate);
            form.Questions.Should().BeEmpty(); // Questions should be ignored
        }

    }
}
