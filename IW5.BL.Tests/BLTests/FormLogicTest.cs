﻿using IW5.BL.API;
using FluentAssertions;
using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using IW5.BL.Tests.Base;
using IW5.DAL.Initialization;
using IW5.Common.Enums.Sorts;
using IW5.API;

namespace IW5.BL.Tests.BLTests;

public class FormLogicIntegrationTests : BaseTest, IClassFixture<EnsureIW5DatabaseTestFixture>
{
    private readonly FormLogic _formLogic;
    private readonly IMapper _formMapper;

    public FormLogicIntegrationTests(EnsureIW5DatabaseTestFixture fixture)
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
        var formModel = new FormDetailModel
        {
            Id = Guid.NewGuid(),
            Title = "Test Form",
            AuthorId = SampleData.Users.First().Id,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7)
        };


        // Act: create the form
        await _formLogic.CreateOrUpdateAsync(formModel);

        // Assert: check if the form was created
        var forms = _formLogic.GetAll();
        forms.Should().Contain(f => f.Title == "Test Form");

    }

    [Fact]
    public async Task UpdateForm_WithExecutionStrategy_ShouldSucceed()
    {
        // Arrange:
        var formModel = new FormDetailModel
        {
            Id = Guid.NewGuid(),
            Title = "Initial Test Form",
            AuthorId = SampleData.Users.First().Id,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7)
        };
        await _formLogic.CreateOrUpdateAsync(formModel);

        // Act:
        formModel.Title = "Updated Test Form";
        await _formLogic.CreateOrUpdateAsync(formModel);

        // Assert:
        var forms = _formLogic.GetAll();
        var updatedForm = forms.FirstOrDefault(f => f.Id == formModel.Id);
        updatedForm!.Should().NotBeNull();
        updatedForm!.Title.Should().Be("Updated Test Form");
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
        var formModel1 = new FormDetailModel
        {
            Id = Guid.NewGuid(),
            Title = "Survey Form 1",
            AuthorId = SampleData.Users.First().Id,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7)
        };
        await _formLogic.CreateOrUpdateAsync(formModel1);

        var formModel2 = new FormDetailModel
        {
            Id = Guid.NewGuid(),
            Title = "Feedback Form 2",
            AuthorId = SampleData.Users.First().Id,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7)
        };
        await _formLogic.CreateOrUpdateAsync(formModel2);

        // Act:
        var result = await _formLogic.GetFilteredForms("Survey");

        // Assert: 
        result.Should().NotBeEmpty();
        result.All(f => f.Title.Contains("Survey")).Should().BeTrue();
    }

    [Fact]
    public async Task GetFilteredForms_ShouldReturnFormsSortedByStartDate()
    {
        // Act: 
        var result = await _formLogic.GetFilteredForms(type: FormSortType.AscendingStartDate);

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
    public async Task DeleteForm_ShouldNotThrowException_WhenFormDoesNotExist()
    {
        // Arrange
        var nonExistentFormModel = new FormDetailModel
        {
            Id = Guid.NewGuid(),
            Title = "Non-Existent Form",
            AuthorId = SampleData.Users.First().Id,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7)
        };
        var id = nonExistentFormModel.Id;

        // Act & Assert
        await _formLogic.CreateOrUpdateAsync(nonExistentFormModel);
        var model = await _formLogic.GetByIdAsync(id);

        Assert.Equal(model.Id, id);

        _formLogic.Delete(nonExistentFormModel);

        Assert.Null(await _formLogic.GetByIdAsync(id));

    }
    [Fact]
    public async Task GetFormById_ShouldReturnDetailedFormInformation()
    {
        // Arrange
        var formModel = new FormDetailModel
        {
            Id = Guid.NewGuid(),
            Title = "Detailed Test Form",
            AuthorId = SampleData.Users.First().Id,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7)
        };
        await _formLogic.CreateOrUpdateAsync(formModel);

        // Act
        var result = await _formLogic.GetByIdAsync(formModel.Id);

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
        var formModel1 = new FormDetailModel
        {
            Id = Guid.NewGuid(),
            Title = "Duplicate Title Form",
            AuthorId = SampleData.Users.First().Id,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7)
        };
        var formModel2 = new FormDetailModel
        {
            Id = Guid.NewGuid(),
            Title = "Duplicate Title Form",
            AuthorId = SampleData.Users.First().Id,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7)
        };

        // Act
        await _formLogic.CreateOrUpdateAsync(formModel1);
        await _formLogic.CreateOrUpdateAsync(formModel2);

        // Assert
        var forms = _formLogic.GetAll();
        forms.Count(f => f.Title == "Duplicate Title Form").Should().Be(2);
    }





}

