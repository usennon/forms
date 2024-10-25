using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using IW5.Models.Entities;
using FluentAssertions;
using AutoMapper;
using IW5.API;

namespace IW5.BL.Tests.MapperTests
{
    public class OptionMapperTest : BaseMapperTest
    {
        private readonly IMapper _optionMapper;
        public OptionMapperTest() : base()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _optionMapper = config.CreateMapper();
        }

        [Fact]
        public void Option_To_OptionListModel_ShouldMapCorrectly()
        {
            // Arrange
            var option = new Option
            {
                Id = Guid.NewGuid(),
                Text = "Option 1"
            };

            // Act
            var optionListModel = _optionMapper.Map<OptionListModel>(option);

            // Assert
            optionListModel.Text.Should().Be(option.Text);
            optionListModel.IsCheked.Should().BeFalse(); // By default, IsCheked should be false
        }

        [Fact]
        public void List_Of_Options_To_List_Of_OptionListModel_ShouldMapCorrectly()
        {
            // Arrange
            var options = new List<Option>
        {
            new Option
            {
                Id = Guid.NewGuid(),
                Text = "Option 1"
            },
            new Option
            {
                Id = Guid.NewGuid(),
                Text = "Option 2"
            }
        };

            // Act
            var optionListModels = _optionMapper.Map<List<OptionListModel>>(options);

            // Assert
            optionListModels.Should().HaveCount(2);
            optionListModels[0].Text.Should().Be(options[0].Text);
            optionListModels[1].Text.Should().Be(options[1].Text);
        }

        [Fact]
        public void Option_With_MinimalFields_To_OptionListModel_ShouldMapCorrectly()
        {
            // Arrange
            var option = new Option
            {
                Id = Guid.NewGuid(),
                Text = "Minimal Option"
            };

            // Act
            var optionListModel = _optionMapper.Map<OptionListModel>(option);

            // Assert
            optionListModel.Text.Should().Be(option.Text);
            optionListModel.IsCheked.Should().BeFalse(); // Default value for IsCheked is false
        }

        [Fact]
        public void Empty_List_Of_Options_To_List_Of_OptionListModel_ShouldMapAsEmptyList()
        {
            // Arrange
            var options = new List<Option>(); // Empty list of options

            // Act
            var optionListModels = _optionMapper.Map<List<OptionListModel>>(options);

            // Assert
            optionListModels.Should().BeEmpty(); // Result should be an empty list
        }

        [Fact]
        public void Null_OptionListModel_ShouldMapCorrectly()
        {
            // Arrange
            Option option = null;

            // Act
            var optionListModel = _optionMapper.Map<OptionListModel>(option);

            // Assert
            optionListModel.Should().BeNull(); // Null input should map to null output
        }
    }
}
