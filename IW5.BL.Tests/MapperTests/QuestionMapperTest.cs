using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using IW5.API;
using IW5.Models.Entities;
using FluentAssertions;
using AutoMapper;


namespace IW5.BL.Tests.MapperTests
{
    public class QuestionMapperTest : BaseMapperTest
    {
        private readonly IMapper _questionMapper;

        public QuestionMapperTest() : base()
        {
            var config = new MapperConfiguration(
                cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _questionMapper = config.CreateMapper();
        }

        [Fact]
        public void Question_To_QuestionDetailModel_ShouldMapCorrectly()
        {
            // Arrange
            var question = new Question
            {
                Id = Guid.NewGuid(),
                Text = "What is your favorite color?",
                Description = "Please select one option.",
                IsRequired = true,
                Options = new List<Option>
            {
                new Option { Id = Guid.NewGuid(), Text = "Red"},
                new Option { Id = Guid.NewGuid(), Text = "Blue"}
            }
            };

            // Act
            var questionDetailModel = _questionMapper.Map<QuestionDetailModel>(question);

            // Assert
            questionDetailModel.Text.Should().Be(question.Text);
            questionDetailModel.Description.Should().Be(question.Description);
            questionDetailModel.IsRequired.Should().Be(question.IsRequired);
            questionDetailModel.Options.Should().HaveCount(2);
            questionDetailModel.Options[0].Text.Should().Be(question.Options.ToList()[0].Text);
            questionDetailModel.Options[1].Text.Should().Be(question.Options.ToList()[1].Text);
        }

        [Fact]
        public void Question_To_QuestionDetailModel_WithEmptyOptions_ShouldMapCorrectly()
        {
            // Arrange
            var question = new Question
            {
                Id = Guid.NewGuid(),
                Text = "What is your favorite food?",
                Description = "Please select one option.",
                IsRequired = false,
                Options = new List<Option>() // Empty options
            };

            // Act
            var questionDetailModel = _questionMapper.Map<QuestionDetailModel>(question);

            // Assert
            questionDetailModel.Text.Should().Be(question.Text);
            questionDetailModel.Description.Should().Be(question.Description);
            questionDetailModel.IsRequired.Should().Be(question.IsRequired);
            questionDetailModel.Options.Should().BeEmpty(); // Options list should be empty
        }

        [Fact]
        public void Question_To_QuestionDetailModel_WithNullOptions_ShouldMapAsEmptyList()
        {
            // Arrange
            var question = new Question
            {
                Id = Guid.NewGuid(),
                Text = "What is your favorite season?",
                Description = "Please select one option.",
                IsRequired = true,
                Options = null // Null options
            };

            // Act
            var questionDetailModel = _questionMapper.Map<QuestionDetailModel>(question);

            // Assert
            questionDetailModel.Text.Should().Be(question.Text);
            questionDetailModel.Description.Should().Be(question.Description);
            questionDetailModel.IsRequired.Should().Be(question.IsRequired);
            questionDetailModel.Options.Should().BeEmpty(); // Null should map to an empty list
        }

        [Fact]
        public void QuestionDetailModel_To_Question_ShouldIgnoreOptions()
        {
            // Arrange
            var questionDetailModel = new QuestionDetailModel
            {
                Text = "Do you like programming?",
                Description = "Yes or No",
                IsRequired = true,
                Options = new List<OptionListModel>
            {
                new OptionListModel { Text = "Yes", IsCheked = true },
                new OptionListModel { Text = "No", IsCheked = false }
            }
            };

            // Act
            var question = _questionMapper.Map<Question>(questionDetailModel);

            // Assert
            question.Text.Should().Be(questionDetailModel.Text);
            question.Description.Should().Be(questionDetailModel.Description);
            question.IsRequired.Should().Be(questionDetailModel.IsRequired);
            question.Options.Should().BeEmpty(); // Options should be ignored
        }

        [Fact]
        public void QuestionDetailModel_To_Question_WithMinimalFields_ShouldMapCorrectly()
        {
            // Arrange
            var questionDetailModel = new QuestionDetailModel
            {
                Text = "Minimal Question",
                IsRequired = true,
                Options = new List<OptionListModel>() // Empty options
            };

            // Act
            var question = _questionMapper.Map<Question>(questionDetailModel);

            // Assert
            question.Text.Should().Be(questionDetailModel.Text);
            question.Description.Should().BeNull(); // Description is not provided, should be null
            question.IsRequired.Should().Be(questionDetailModel.IsRequired);
            question.Options.Should().BeEmpty(); // Options should be empty
        }
        [Fact]
        public void Question_To_QuestionListModel_ShouldMapCorrectly()
        {
            // Arrange
            var question = new Question
            {
                Id = Guid.NewGuid(),
                Text = "What is your favorite color?",
                IsRequired = true
            };

            // Act
            var questionListModel = _questionMapper.Map<QuestionListModel>(question);

            // Assert
            questionListModel.Text.Should().Be(question.Text);
            questionListModel.IsRequired.Should().Be(question.IsRequired);
        }

        [Fact]
        public void List_Of_Questions_To_List_Of_QuestionListModel_ShouldMapCorrectly()
        {
            // Arrange
            var questions = new List<Question>
        {
            new Question
            {
                Id = Guid.NewGuid(),
                Text = "What is your favorite color?",
                IsRequired = true
            },
            new Question
            {
                Id = Guid.NewGuid(),
                Text = "What is your favorite food?",
                IsRequired = false
            }
        };

            // Act
            var questionListModels = _questionMapper.Map<List<QuestionListModel>>(questions);

            // Assert
            questionListModels.Should().HaveCount(2);
            questionListModels[0].Text.Should().Be(questions[0].Text);
            questionListModels[0].IsRequired.Should().Be(questions[0].IsRequired);
            questionListModels[1].Text.Should().Be(questions[1].Text);
            questionListModels[1].IsRequired.Should().Be(questions[1].IsRequired);
        }

        [Fact]
        public void Question_With_MinimalFields_To_QuestionListModel_ShouldMapCorrectly()
        {
            // Arrange
            var question = new Question
            {
                Id = Guid.NewGuid(),
                Text = "Minimal Question" // Only text and ID set
            };

            // Act
            var questionListModel = _questionMapper.Map<QuestionListModel>(question);

            // Assert
            questionListModel.Text.Should().Be(question.Text);
            questionListModel.IsRequired.Should().BeFalse(); // Default value of IsRequired should be false
        }

        [Fact]
        public void Empty_List_Of_Questions_To_List_Of_QuestionListModel_ShouldMapAsEmptyList()
        {
            // Arrange
            var questions = new List<Question>(); // Empty list of questions

            // Act
            var questionListModels = _questionMapper.Map<List<QuestionListModel>>(questions);

            // Assert
            questionListModels.Should().BeEmpty(); // Result should be an empty list
        }
    }
}
