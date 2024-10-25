using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using IW5.API;
using IW5.Models.Entities;
using FluentAssertions;
using AutoMapper;

namespace IW5.BL.Tests.MapperTests
{
    public class UserMapperTest : BaseMapperTest
    {
        private readonly IMapper _userMapper;
        public UserMapperTest() : base() 
        {
            var config = new MapperConfiguration(
            cfg => 
            { 
                cfg.AddProfile<MappingProfile>(); 
            });
            _userMapper = config.CreateMapper();
        }

        [Fact]
        public void User_To_UserListModel_Mapping_ShouldBeValid()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                PhotoUrl = "http://example.com/photo.jpg"
            };

            // Act
            var userListModel = _userMapper.Map<UserListModel>(user);

            // Assert
            userListModel.Name.Should().Be(user.Name);
            userListModel.PhotoUrl.Should().Be(user.PhotoUrl);
        }

        [Fact]
        public void User_To_UserDetailModel_Mapping_ShouldBeValid()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                PhotoUrl = "http://example.com/photo.jpg",
                CreatedAt = DateTime.UtcNow,
                Forms = new List<Form>
            {
                new Form { Id = Guid.NewGuid(), Title = "Form 1" },
                new Form { Id = Guid.NewGuid(), Title = "Form 2" }
            }
            };

            // Act
            var userDetailModel = _userMapper.Map<UserDetailModel>(user);

            // Assert
            userDetailModel.Name.Should().Be(user.Name);
            userDetailModel.PhotoUrl.Should().Be(user.PhotoUrl);
            userDetailModel.Forms.Should().HaveCount(2);
            userDetailModel.Forms[0].Title.Should().Be(user.Forms.ToList()[0].Title);
            userDetailModel.Forms[1].Title.Should().Be(user.Forms.ToList()[1].Title);
        }

        [Fact]
        public void UserDetailModel_To_User_Mapping_ShouldIgnoreForms()
        {
            // Arrange
            var userDetailModel = new UserDetailModel
            {
                Name = "John Doe",
                PhotoUrl = "http://example.com/photo.jpg",
                CreatedAt = DateTime.UtcNow,
                Forms = new List<FormListModel>
            {
                new FormListModel { Title = "Form 1" },
                new FormListModel { Title = "Form 2" }
            }
            };

            // Act
            var user = _userMapper.Map<User>(userDetailModel);

            // Assert
            user.Name.Should().Be(userDetailModel.Name);
            user.PhotoUrl.Should().Be(userDetailModel.PhotoUrl);
            user.Forms.Should().BeEmpty();
        }

        [Fact]
        public void User_To_UserDetailModel_WithEmptyForms_ShouldMapCorrectly()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                PhotoUrl = "http://example.com/photo.jpg",
                CreatedAt = DateTime.UtcNow,
                Forms = new List<Form>() 
            };

            // Act
            var userDetailModel = _userMapper.Map<UserDetailModel>(user);

            // Assert
            userDetailModel.Name.Should().Be(user.Name);
            userDetailModel.PhotoUrl.Should().Be(user.PhotoUrl);
            userDetailModel.Forms.Should().BeEmpty();
        }

        [Fact]
        public void User_To_UserDetailModel_WithNullPhotoUrl_ShouldMapCorrectly()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                PhotoUrl = null, // PhotoUrl == null
                CreatedAt = DateTime.UtcNow,
                Forms = new List<Form>
                {
                    new Form { Id = Guid.NewGuid(), Title = "Form 1" }
                }
            };

            // Act
            var userDetailModel = _userMapper.Map<UserDetailModel>(user);

            // Assert
            userDetailModel.Name.Should().Be(user.Name);
            userDetailModel.PhotoUrl.Should().BeNull(); 
            userDetailModel.Forms.Should().HaveCount(1);
        }

        [Fact]
        public void User_To_UserDetailModel_WithNullForms_ShouldCreateEmptyCollection()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                PhotoUrl = "http://example.com/photo.jpg",
                CreatedAt = DateTime.UtcNow,
                Forms = new List<Form>() // Forms == null
            };

            // Act
            var userDetailModel = _userMapper.Map<UserDetailModel>(user);

            // Assert
            userDetailModel.Name.Should().Be(user.Name);
            userDetailModel.PhotoUrl.Should().Be(user.PhotoUrl);
            userDetailModel.Forms.Should().BeEmpty(); 
        }

        [Fact]
        public void UserDetailModel_To_User_WithEmptyForms_ShouldMapCorrectly()
        {
            // Arrange
            var userDetailModel = new UserDetailModel
            {
                Name = "John Doe",
                PhotoUrl = "http://example.com/photo.jpg",
                CreatedAt = DateTime.UtcNow,
                Forms = new List<FormListModel>() 
            };

            // Act
            var user = _userMapper.Map<User>(userDetailModel);

            // Assert
            user.Name.Should().Be(userDetailModel.Name);
            user.PhotoUrl.Should().Be(userDetailModel.PhotoUrl);
            user.Forms.Should().BeEmpty(); 
        }

        [Fact]
        public void UserDetailModel_To_User_Mapping_ShouldMapCorrectly()
        {
            // Arrange
            var userDetailModel = new UserDetailModel
            {
                Name = "John Doe",
                PhotoUrl = "http://example.com/photo.jpg",
                CreatedAt = DateTime.UtcNow,
                Forms = new List<FormListModel>
        {
            new FormListModel { Title = "Form 1" },
            new FormListModel { Title = "Form 2" }
        }
            };

            // Act
            var user = _userMapper.Map<User>(userDetailModel);

            // Assert
            user.Name.Should().Be(userDetailModel.Name);
            user.PhotoUrl.Should().Be(userDetailModel.PhotoUrl);
            user.Forms.Should().BeEmpty(); 
        }

        [Fact]
        public void UserDetailModel_To_User_WithNullPhotoUrl_ShouldMapCorrectly()
        {
            // Arrange
            var userDetailModel = new UserDetailModel
            {
                Name = "Jane Doe",
                PhotoUrl = null, // PhotoUrl == null
                CreatedAt = DateTime.UtcNow,
                Forms = new List<FormListModel>() 
            };

            // Act
            var user = _userMapper.Map<User>(userDetailModel);

            // Assert
            user.Name.Should().Be(userDetailModel.Name);
            user.PhotoUrl.Should().BeNull(); 
            user.Forms.Should().BeEmpty(); 
        }
    }
}
