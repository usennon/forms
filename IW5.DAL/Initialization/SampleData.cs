using IW5.Models.Entities;
using IW5.Common.Enums;

namespace IW5.Dal.Initialization
{
    public static class SampleData
    {
        // Sample Users
        public static List<User> Users => new()
        {
            new() { Id = Guid.Parse("980745cb-b407-4b72-9a6b-1d5c9cf6a5ef"), Name = "John Doe", Email = "johndoe@example.com", Role = Role.Basic, CreatedAt = DateTime.UtcNow },
            new() { Id = Guid.Parse("c2ad823a-c3bc-49cb-a930-2fd719c0e997"), Name = "Jane Smith", Email = "janesmith@example.com", Role = Role.Admin, CreatedAt = DateTime.UtcNow },
        };

        // Sample Forms
        public static List<Form> Forms => new()
        {
            new()
            {
                Id = Guid.Parse("be495716-bdff-48d2-9dc7-d9c51deba0f7"),
                Title = "Employee Satisfaction Survey",
                AuthorId = Users[0].Id,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMonths(1),
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.Parse("e3b27283-30b4-4522-80bc-0eb3d6409c35"),
                Title = "Product Feedback Form",
                AuthorId = Users[1].Id,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMonths(1),
                CreatedAt = DateTime.UtcNow
            }
        };

        // Sample Questions
        public static List<Question> Questions => new()
        {
            new()
            {
                Id = Guid.Parse("5bc27217-6817-40e4-b8d1-60dc9aca3e83"),
                Text = "How satisfied are you with your job?",
                Type = QuestionType.MultipleChoice,
                IsRequired = true,
                FormId = Forms[0].Id,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.Parse("59fe555e-3bcc-4ace-b9fc-68b76805ac59"),
                Text = "Would you recommend this product?",
                Type = QuestionType.YesNo,
                IsRequired = true,
                FormId = Forms[1].Id,
                CreatedAt = DateTime.UtcNow
            }
        };

        // Sample Options
        public static List<Option> Options => new()
        {
            new() { Id = Guid.NewGuid(), Text = "Very satisfied", QuestionId = Questions[0].Id, CreatedAt = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Text = "Satisfied", QuestionId = Questions[0].Id, CreatedAt = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Text = "Neutral", QuestionId = Questions[0].Id, CreatedAt = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Text = "Yes", QuestionId = Questions[1].Id, CreatedAt = DateTime.UtcNow },
            new() { Id = Guid.NewGuid(), Text = "No", QuestionId = Questions[1].Id, CreatedAt = DateTime.UtcNow }
        };
    }
}
