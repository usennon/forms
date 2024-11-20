using IW5.BL.API;
using FluentAssertions;
using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using IW5.BL.Tests.Base;
using IW5.DAL.Initialization;
using IW5.Common.Enums.Sorts;
using IW5.API;
using IW5.BL.Models.ManipulationModels.FormsModels;

namespace IW5.BL.Tests.BLTests;

public class FormLogicIntegrationTests : BaseTest, IClassFixture<EnsureIW5DatabaseTestFixture>
{
    private readonly FormLogic _formLogic;
    private readonly IMapper _formMapper;

    public FormLogicIntegrationTests(EnsureIW5DatabaseTestFixture fixture) : base(fixture)
    {
        var config = new MapperConfiguration(
        cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _formMapper = config.CreateMapper();
        _formLogic = new FormLogic(_repositoryManager, _formMapper);
    }
    [Fact]
    public async Task CreateForm_WithExecutionStrategy_ShouldSucceed()
    {
        // Arrange
        var formModel = new FormForManipulationModel
        {
            Title = "Test Form",
            AuthorId = SampleData.Users.First().Id,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7)
        };


        // Act: create the form
        await _formLogic.Create(formModel);

        // Assert: check if the form was created
        var forms = _formLogic.GetAll();
        forms.Should().Contain(f => f.Title == "Test Form");

    }



    [Fact]
    public void GetAllForms_ShouldReturnAllForms()
    {
        // Act: 
        var forms = _formLogic.GetAll();

        // Assert: 
        forms.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetFilteredForms_ShouldReturnFormsContainingSubstring()
    {
        // Arrange:
        var formModel1 = new FormForManipulationModel
        {
            Title = "Survey Form 1",
            AuthorId = SampleData.Users.First().Id,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7)
        };
        await _formLogic.Create(formModel1);

        var formModel2 = new FormForManipulationModel
        {
            Title = "Feedback Form 2",
            AuthorId = SampleData.Users.First().Id,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7)
        };
        await _formLogic.Create(formModel2);

        // Act:
        var result = _formLogic.GetFilteredForms("Survey");

        // Assert: 
        result.Should().NotBeEmpty();
        result.All(f => f.Title.Contains("Survey")).Should().BeTrue();
    }

    [Fact]
    public void GetFilteredForms_ShouldReturnFormsSortedByStartDate()
    {
        // Act: 
        var result =  _formLogic.GetFilteredForms(type: FormSortType.AscendingStartDate);

        // Assert: 
        result.Should().NotBeEmpty();
        result.Should().BeEquivalentTo(result, options => options.WithStrictOrdering());
    }
    [Fact]
    public async Task GetFormById_ShouldReturnNull_WhenFormDoesNotExist()
    {
        // Arrange
        var nonExistentFormId = Guid.NewGuid();

        // Act
        var result = await _formLogic.GetByIdAsync(nonExistentFormId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetFormById_ShouldReturnDetailedFormInformation()
    {
        // Arrange
        var formModel = new FormForManipulationModel
        {
            Title = "Detailed Test Form",
            AuthorId = SampleData.Users.First().Id,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7)
        };
        await _formLogic.Create(formModel);

        // Act
        var result = await _formLogic.GetFormByTitleAsync(formModel.Title);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("Detailed Test Form");
        result.AuthorId.Should().Be(formModel.AuthorId);
        result.StartDate.Should().BeCloseTo(formModel.StartDate, TimeSpan.FromMilliseconds(100));
        result.EndDate.Should().BeCloseTo(formModel.EndDate, TimeSpan.FromMilliseconds(100));
    }
    [Fact]
    public async Task CreateMultipleForms_WithSameTitle_ShouldSucceed()
    {
        // Arrange
        var formModel1 = new FormForManipulationModel
        {
            Title = "Duplicate Title Form",
            AuthorId = SampleData.Users.First().Id,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7)
        };
        var formModel2 = new FormForManipulationModel
        {
            Title = "Duplicate Title Form",
            AuthorId = SampleData.Users.First().Id,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7)
        };

        // Act
        await _formLogic.Create(formModel1);
        await _formLogic.Create(formModel2);

        // Assert
        var forms = _formLogic.GetAll();
        forms.Count(f => f.Title == "Duplicate Title Form").Should().Be(2);
    }





}

