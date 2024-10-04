using IW5.Models.Entities;
using IW5.Common.Enums;

namespace IW5.Dal.Initialization
{
    public static class SampleData
    {
        // Sample Users
        public static List<User> Users => new()
        {
            new User
            {
                Id = Guid.Parse("980745cb-b407-4b72-9a6b-1d5c9cf6a5ef"),
                Name = "John Doe",
                Email = "johndoe@example.com",
                Role = Role.Basic,
                CreatedAt = new DateTime(2021, 5, 12, 8, 30, 0)  // Predefined Date
            },
            new User
            {
                Id = Guid.Parse("c2ad823a-c3bc-49cb-a930-2fd719c0e997"),
                Name = "Jane Smith",
                Email = "janesmith@example.com",
                Role = Role.Basic,
                CreatedAt = new DateTime(2020, 11, 25, 14, 15, 0)  // Predefined Date
            },
            new User
            {
                Id = Guid.Parse("ad2d34eb-d2a8-4e0a-9a17-c0d295d8995a"),
                Name = "Bob Johnson",
                Email = "bobjohnson@example.com",
                Role = Role.Basic,
                CreatedAt = new DateTime(2022, 1, 18, 9, 0, 0)  // Predefined Date
            },
            new User
            {
                Id = Guid.Parse("e920f477-7f53-4bba-b1b6-d8d9376b4d30"),
                Name = "Alice Williams",
                Email = "alicewilliams@example.com",
                Role = Role.Admin,
                CreatedAt = new DateTime(2023, 3, 10, 16, 45, 0)  // Predefined Date
            },
            new User
            {
                Id = Guid.Parse("7d5a7f7b-4a0d-41b6-9b9f-02c68c5d8b98"),
                Name = "Chris Evans",
                Email = "chrisevans@example.com",
                Role = Role.Basic,
                CreatedAt = new DateTime(2021, 7, 8, 10, 30, 0)  // Predefined Date
            },
            new User
            {
                Id = Guid.Parse("c60e1c3e-4632-499f-b948-103558d91c5e"),
                Name = "Emily Clark",
                Email = "emilyclark@example.com",
                Role = Role.Admin,
                CreatedAt = new DateTime(2019, 9, 22, 11, 0, 0)  // Predefined Date
            }
        };

        // Sample Forms
        public static List<Form> Forms => new()
        {
            new Form
            {
                Id = Guid.Parse("df1c5c1d-7a55-4b4a-8e2e-bd3f31230f71"),
                Title = "Marketing Campaign Effectiveness",
                AuthorId = Users[0].Id,
                StartDate = new DateTime(2022, 3, 12, 10, 0, 0),
                EndDate = new DateTime(2022, 3, 12, 12, 30, 0),
                CreatedAt = new DateTime(2022, 3, 11, 14, 0, 0)
            },
            new Form
            {
                Id = Guid.Parse("6f28e7f7-42b2-4d98-a6ae-2fe7d7d6fd6c"),
                Title = "Remote Work Feedback Survey",
                AuthorId = Users[0].Id,
                StartDate = new DateTime(2022, 5, 1, 9, 0, 0),
                EndDate = new DateTime(2022, 5, 1, 11, 0, 0),
                CreatedAt = new DateTime(2022, 4, 29, 8, 0, 0)
            },
            new Form
            {
                Id = Guid.Parse("982f69fd-6278-4e5d-b896-97e1be5b17d2"),
                Title = "Weekly Team Retrospective",
                AuthorId = Users[0].Id,
                StartDate = new DateTime(2022, 4, 18, 10, 0, 0),
                EndDate = new DateTime(2022, 4, 18, 11, 30, 0),
                CreatedAt = new DateTime(2022, 4, 17, 9, 0, 0)
            },
            new Form
            {
                Id = Guid.Parse("a6d5f2e1-c4b1-404e-832d-ebe9f1d0e1d5"),
                Title = "Client Satisfaction Poll Q2",
                AuthorId = Users[0].Id,
                StartDate = new DateTime(2022, 6, 10, 13, 0, 0),
                EndDate = new DateTime(2022, 6, 10, 15, 0, 0),
                CreatedAt = new DateTime(2022, 6, 9, 11, 0, 0)
            },
            new Form
            {
                Id = Guid.Parse("7e39599c-44a6-4e2e-9890-5a98117b5d34"),
                Title = "Innovation Challenge Submission",
                AuthorId = Users[1].Id,
                StartDate = new DateTime(2022, 7, 20, 10, 0, 0),
                EndDate = new DateTime(2022, 7, 20, 17, 0, 0),
                CreatedAt = new DateTime(2022, 7, 18, 16, 0, 0)
            },
            new Form
            {
                Id = Guid.Parse("ff86e3f1-405d-4f98-8db2-6fa75660c0f3"),
                Title = "Safety Awareness Training Quiz",
                AuthorId = Users[1].Id,
                StartDate = new DateTime(2022, 9, 30, 14, 0, 0),
                EndDate = new DateTime(2022, 9, 30, 16, 0, 0),
                CreatedAt = new DateTime(2022, 9, 29, 10, 0, 0)
            },
            new Form
            {
                Id = Guid.Parse("fdcb8b0f-6532-4f91-b2a6-97035e36c04d"),
                Title = "End of Year Feedback Form",
                AuthorId = Users[2].Id,
                StartDate = new DateTime(2022, 12, 15, 9, 0, 0),
                EndDate = new DateTime(2022, 12, 15, 11, 30, 0),
                CreatedAt = new DateTime(2022, 12, 14, 10, 0, 0)
            },
            new Form
            {
                Id = Guid.Parse("2d76f973-e0a4-4d16-b0d9-7469fa9de8b1"),
                Title = "Leadership Evaluation Form",
                AuthorId = Users[2].Id,
                StartDate = new DateTime(2022, 8, 22, 9, 0, 0),
                EndDate = new DateTime(2022, 8, 22, 12, 0, 0),
                CreatedAt = new DateTime(2022, 8, 20, 10, 0, 0)
            },
            new Form
            {
                Id = Guid.Parse("b3d6b791-bd93-48ae-8de2-44b6e79fe315"),
                Title = "IT System Upgrade Feedback",
                AuthorId = Users[2].Id,
                StartDate = new DateTime(2022, 10, 5, 14, 0, 0),
                EndDate = new DateTime(2022, 10, 5, 17, 0, 0),
                CreatedAt = new DateTime(2022, 10, 3, 13, 0, 0)
            },
            new Form
            {
                Id = Guid.Parse("a7b1a3f5-6bb2-4dbf-9cc5-2a34c1ebfcfa"),
                Title = "Customer Experience Survey Q3",
                AuthorId = Users[3].Id,
                StartDate = new DateTime(2022, 9, 12, 10, 0, 0),
                EndDate = new DateTime(2022, 9, 12, 12, 30, 0),
                CreatedAt = new DateTime(2022, 9, 10, 11, 0, 0)
            },
            new Form
            {
                Id = Guid.Parse("c9f89913-d2e9-46b7-b013-30b9d256d502"),
                Title = "Employee Skills Assessment",
                AuthorId = Users[4].Id,
                StartDate = new DateTime(2022, 11, 5, 9, 0, 0),
                EndDate = new DateTime(2022, 11, 5, 11, 30, 0),
                CreatedAt = new DateTime(2022, 11, 3, 8, 0, 0)
            },
            new Form
            {
                Id = Guid.Parse("7f46a3f3-fbaf-47bb-b38c-8790567bc924"),
                Title = "Annual Employee Benefits Review",
                AuthorId = Users[4].Id,
                StartDate = new DateTime(2022, 12, 20, 10, 0, 0),
                EndDate = new DateTime(2022, 12, 20, 14, 0, 0),
                CreatedAt = new DateTime(2022, 12, 18, 9, 0, 0)
            },
            new Form
            {
                Id = Guid.Parse("8d92f4ff-913a-4e5a-b399-0ffdbb04c688"),
                Title = "Company Culture Survey",
                AuthorId = Users[4].Id,
                StartDate = new DateTime(2023, 1, 15, 13, 0, 0),
                EndDate = new DateTime(2023, 1, 15, 15, 30, 0),
                CreatedAt = new DateTime(2023, 1, 14, 11, 0, 0)
            },
            new Form
            {
                Id = Guid.Parse("34a2a6b7-84e6-4ffb-9cd7-f506e7f853b7"),
                Title = "Project Retrospective 2023",
                AuthorId = Users[5].Id,
                StartDate = new DateTime(2023, 3, 5, 9, 0, 0),
                EndDate = new DateTime(2023, 3, 5, 11, 30, 0),
                CreatedAt = new DateTime(2023, 3, 4, 8, 0, 0)
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
                CreatedAt = new DateTime(2021, 10, 8, 12, 0, 0)
            },
            new()
            {
                Id = Guid.Parse("59fe555e-3bcc-4ace-b9fc-68b76805ac59"),
                Text = "Would you recommend this product?",
                Type = QuestionType.YesNo,
                IsRequired = true,
                FormId = Forms[1].Id,
                CreatedAt = new DateTime(2021, 10, 8, 12, 0, 0)
            },
            new Question
            {
                Id = Guid.Parse("b4e645e1-4a28-45a2-9aa3-3b76af8a5f12"),
                Text = "How effective do you think the recent marketing campaign was?",
                Type = QuestionType.MultipleChoice,
                IsRequired = true,
                FormId = Guid.Parse("df1c5c1d-7a55-4b4a-8e2e-bd3f31230f71"), // Marketing Campaign Effectiveness FormId
                CreatedAt = new DateTime(2022, 3, 12, 11, 15, 0)
            },
            new Question
            {
                Id = Guid.Parse("fd3cdefe-4f69-40f4-86fa-b2a3ad0b02f8"),
                Text = "What are the biggest challenges you face while working remotely?",
                Type = QuestionType.Text,
                IsRequired = true,
                FormId = Guid.Parse("6f28e7f7-42b2-4d98-a6ae-2fe7d7d6fd6c"), // Remote Work Feedback Survey FormId
                CreatedAt = new DateTime(2022, 5, 1, 10, 0, 0)
            },
            new Question
            {
                Id = Guid.Parse("8b6c94e7-5ea9-4e56-a0c6-5586f01fa570"),
                Text = "Do you feel that your team's collaboration has improved over the past week?",
                Type = QuestionType.YesNo,
                IsRequired = true,
                FormId = Guid.Parse("982f69fd-6278-4e5d-b896-97e1be5b17d2"), // Weekly Team Retrospective FormId
                CreatedAt = new DateTime(2022, 4, 18, 12, 0, 0)
            },
            new Question
            {
                Id = Guid.Parse("be7426eb-8305-46f3-9d59-dbd2bf0d6fa3"),
                Text = "How would you rate our customer support on a scale of 1 to 10?",
                Type = QuestionType.MultipleChoice,
                IsRequired = true,
                FormId = Guid.Parse("a6d5f2e1-c4b1-404e-832d-ebe9f1d0e1d5"), // Client Satisfaction Poll Q2 FormId
                CreatedAt = new DateTime(2022, 6, 10, 14, 0, 0)
            },
            new Question
            {
                Id = Guid.Parse("5d3aebd4-df8f-4d1a-8cc3-b784e3fbaf66"),
                Text = "What safety protocols would you like to see implemented?",
                Type = QuestionType.Text,
                IsRequired = false,
                FormId = Guid.Parse("ff86e3f1-405d-4f98-8db2-6fa75660c0f3"), // Safety Awareness Training Quiz FormId
                CreatedAt = new DateTime(2022, 9, 30, 10, 0, 0)
            },
            new Question
            {
                Id = Guid.Parse("0b6dd598-9112-4c7a-8f58-b19a3a31c3ea"),
                Text = "Was there any particular achievement you're proud of this year?",
                Type = QuestionType.Text,
                IsRequired = true,
                FormId = Guid.Parse("fdcb8b0f-6532-4f91-b2a6-97035e36c04d"), // End of Year Feedback FormId
                CreatedAt = new DateTime(2022, 12, 15, 9, 0, 0)
            },
            new Question
            {
                Id = Guid.Parse("219f6a91-3956-4b44-96c8-c19bf8e914d4"),
                Text = "Do you think our innovation challenge was beneficial for you?",
                Type = QuestionType.YesNo,
                IsRequired = true,
                FormId = Guid.Parse("7e39599c-44a6-4e2e-9890-5a98117b5d34"), // Innovation Challenge Submission FormId
                CreatedAt = new DateTime(2022, 7, 20, 11, 30, 0)
            },
            new Question
            {
                Id = Guid.Parse("11d6a217-56f9-45a6-9a3a-fc3c37918e9b"),
                Text = "What new ideas would you suggest for our next product?",
                Type = QuestionType.Text,
                IsRequired = false,
                FormId = Guid.Parse("7e39599c-44a6-4e2e-9890-5a98117b5d34"), // Innovation Challenge Submission FormId
                CreatedAt = new DateTime(2022, 7, 19, 14, 45, 0)
            },
            new Question
            {
                Id = Guid.Parse("fe91bf9d-d8e7-403e-a62d-06f4c60230de"),
                Text = "Was the training session clear and helpful?",
                Type = QuestionType.YesNo,
                IsRequired = true,
                FormId = Guid.Parse("ff86e3f1-405d-4f98-8db2-6fa75660c0f3"), // Safety Awareness Training Quiz FormId
                CreatedAt = new DateTime(2022, 9, 30, 15, 0, 0)
            },
            new Question
            {
                Id = Guid.Parse("73be12f8-2f15-4183-abc1-347c07e7898f"),
                Text = "What could be improved in this week's team performance?",
                Type = QuestionType.Text,
                IsRequired = false,
                FormId = Guid.Parse("982f69fd-6278-4e5d-b896-97e1be5b17d2"), // Weekly Team Retrospective FormId
                CreatedAt = new DateTime(2022, 4, 18, 11, 0, 0)
            },
            new Question
            {
                Id = Guid.Parse("f89c1d7e-0e3d-4871-89b7-82fe5ddfbb7b"),
                Text = "How satisfied are you with our client services?",
                Type = QuestionType.MultipleChoice,
                IsRequired = true,
                FormId = Guid.Parse("a6d5f2e1-c4b1-404e-832d-ebe9f1d0e1d5"), // Client Satisfaction Poll Q2 FormId
                CreatedAt = new DateTime(2022, 6, 9, 10, 0, 0)
            },
            new Question
            {
                Id = Guid.Parse("893fe128-c0fc-4c97-9b35-745e42d0b56a"),
                Text = "Do you feel safe in your work environment?",
                Type = QuestionType.YesNo,
                IsRequired = true,
                FormId = Guid.Parse("ff86e3f1-405d-4f98-8db2-6fa75660c0f3"), // Safety Awareness Training Quiz FormId
                CreatedAt = new DateTime(2022, 9, 29, 15, 30, 0)
            },
            new Question
            {
                Id = Guid.Parse("12df27e9-90be-4985-88db-bc843c6e80b4"),
                Text = "What was your biggest takeaway from the year?",
                Type = QuestionType.Text,
                IsRequired = false,
                FormId = Guid.Parse("fdcb8b0f-6532-4f91-b2a6-97035e36c04d"), // End of Year Feedback FormId
                CreatedAt = new DateTime(2022, 12, 14, 14, 45, 0)
            },
                        new Question
            {
                Id = Guid.Parse("1c5ebfa7-3d76-4b3b-bd0a-e254b828bb0a"),
                Text = "How many campaigns have you participated in this year?",
                Type = QuestionType.Numbers,
                IsRequired = true,
                FormId = Guid.Parse("df1c5c1d-7a55-4b4a-8e2e-bd3f31230f71"), // Marketing Campaign Effectiveness FormId
                CreatedAt = new DateTime(2022, 3, 12, 13, 15, 0)
            },
            new Question
            {
                Id = Guid.Parse("f4d5c8a6-1124-4299-b6a8-0fd9c506d1e4"),
                Text = "Please describe your experience with remote working.",
                Type = QuestionType.Text,
                IsRequired = true,
                FormId = Guid.Parse("6f28e7f7-42b2-4d98-a6ae-2fe7d7d6fd6c"), // Remote Work Feedback Survey FormId
                CreatedAt = new DateTime(2022, 5, 1, 15, 0, 0)
            },
            new Question
            {
                Id = Guid.Parse("a7a4e3fb-9c8c-44f7-b8f8-3e2e7c7f3a4c"),
                Text = "On a scale of 1-10, how would you rate your team's performance this week?",
                Type = QuestionType.Numbers,
                IsRequired = true,
                FormId = Guid.Parse("982f69fd-6278-4e5d-b896-97e1be5b17d2"), // Weekly Team Retrospective FormId
                CreatedAt = new DateTime(2022, 4, 18, 14, 0, 0)
            },
            new Question
            {
                Id = Guid.Parse("ed9f6d5f-78f6-43b8-8f2d-0f6175c54b8a"),
                Text = "Which features of our product do you find most useful?",
                Type = QuestionType.Test,
                IsRequired = false,
                FormId = Guid.Parse("a6d5f2e1-c4b1-404e-832d-ebe9f1d0e1d5"), // Client Satisfaction Poll Q2 FormId
                CreatedAt = new DateTime(2022, 6, 10, 15, 30, 0)
            },
            new Question
            {
                Id = Guid.Parse("7c8f6c1d-d598-4c89-a16d-3e4f456e9fa3"),
                Text = "Is your work environment safe?",
                Type = QuestionType.YesNo,
                IsRequired = true,
                FormId = Guid.Parse("ff86e3f1-405d-4f98-8db2-6fa75660c0f3"), // Safety Awareness Training Quiz FormId
                CreatedAt = new DateTime(2022, 9, 29, 11, 0, 0)
            },
            new Question
            {
                Id = Guid.Parse("219a7c21-9a11-45f1-8c6d-9e2f1f67b3a4"),
                Text = "What are your key achievements for this year?",
                Type = QuestionType.Text,
                IsRequired = false,
                FormId = Guid.Parse("fdcb8b0f-6532-4f91-b2a6-97035e36c04d"), // End of Year Feedback FormId
                CreatedAt = new DateTime(2022, 12, 15, 10, 15, 0)
            },
            new Question
            {
                Id = Guid.Parse("f5b3e6d8-62f3-4f0e-9af6-3c7f9b6e0c3a"),
                Text = "How many safety training sessions have you attended?",
                Type = QuestionType.Numbers,
                IsRequired = true,
                FormId = Guid.Parse("ff86e3f1-405d-4f98-8db2-6fa75660c0f3"), // Safety Awareness Training Quiz FormId
                CreatedAt = new DateTime(2022, 9, 30, 12, 0, 0)
            },
            new Question
            {
                Id = Guid.Parse("fd9b87c1-b4e6-4c8f-a8f5-d78c9a1f2a3c"),
                Text = "Which innovation ideas did you find most interesting?",
                Type = QuestionType.Test,
                IsRequired = true,
                FormId = Guid.Parse("7e39599c-44a6-4e2e-9890-5a98117b5d34"), // Innovation Challenge Submission FormId
                CreatedAt = new DateTime(2022, 7, 20, 10, 0, 0)
            }
        };

        // Sample Options
        public static List<Option> Options => new()
        {
            new() { Id = Guid.Parse("a1c0f4ab-6c4d-4b82-baf2-123c78e50d3a"), Text = "Very satisfied", QuestionId = Questions[2].Id, CreatedAt = new DateTime(2021, 10, 8, 12, 5, 0) },
            new() { Id = Guid.Parse("a1c0f4ab-6c4d-4b82-baf2-123c78e50d3b"), Text = "Satisfied", QuestionId = Questions[2].Id, CreatedAt = new DateTime(2021, 10, 8, 12, 5, 0) },
            new() { Id = Guid.Parse("a1c0f4ab-6c4d-4b82-baf2-123c78e50d3c"), Text = "Neutral", QuestionId = Questions[2].Id, CreatedAt = new DateTime(2021, 10, 8, 12, 5, 0) },
            new() { Id = Guid.Parse("a1c0f4ab-6c4d-4b82-baf2-123c78e50d3d"), Text = "Yes", QuestionId = Questions[1].Id, CreatedAt = new DateTime(2021, 10, 8, 12, 5, 0) },
            new() { Id = Guid.Parse("a1c0f4ab-6c4d-4b82-baf2-123c78e50d3e"), Text = "No", QuestionId = Questions[1].Id, CreatedAt = new DateTime(2021, 10, 8, 12, 5, 0) },
            new Option
            {
                Id = Guid.Parse("a1c0f4ab-6c4d-4b82-baf2-123c78e50d3f"),
                Text = "Very satisfied",
                QuestionId = Guid.Parse("5bc27217-6817-40e4-b8d1-60dc9aca3e83"), // Corresponding to Question "How satisfied are you with your job?"
                CreatedAt = new DateTime(2021, 10, 8, 12, 5, 0)
            },
            new Option
            {
                Id = Guid.Parse("7b9e196e-2f7f-482e-8a2a-340dd39f4fc7"),
                Text = "Satisfied",
                QuestionId = Guid.Parse("5bc27217-6817-40e4-b8d1-60dc9aca3e83"), // Corresponding to Question "How satisfied are you with your job?"
                CreatedAt = new DateTime(2021, 10, 8, 12, 10, 0)
            },
            new Option
            {
                Id = Guid.Parse("f5f1f7b0-1e65-42e1-b9b3-01efc54e1de3"),
                Text = "Neutral",
                QuestionId = Guid.Parse("5bc27217-6817-40e4-b8d1-60dc9aca3e83"), // Corresponding to Question "How satisfied are you with your job?"
                CreatedAt = new DateTime(2021, 10, 8, 12, 15, 0)
            },
            new Option
            {
                Id = Guid.Parse("fcd24794-b03f-4e93-bfe6-896981ed87f7"),
                Text = "Dissatisfied",
                QuestionId = Guid.Parse("5bc27217-6817-40e4-b8d1-60dc9aca3e83"), // Corresponding to Question "How satisfied are you with your job?"
                CreatedAt = new DateTime(2021, 10, 8, 12, 20, 0)
            },
            new Option
            {
                Id = Guid.Parse("26d8a7a8-bc22-48e8-a6e1-7b2e3c4fcb85"),
                Text = "Very dissatisfied",
                QuestionId = Guid.Parse("5bc27217-6817-40e4-b8d1-60dc9aca3e83"), // Corresponding to Question "How satisfied are you with your job?"
                CreatedAt = new DateTime(2021, 10, 8, 12, 25, 0)
            },
            new Option
            {
                Id = Guid.Parse("ea3451f6-9853-4748-b2a3-718b11b0d9b5"),
                Text = "Yes",
                QuestionId = Guid.Parse("59fe555e-3bcc-4ace-b9fc-68b76805ac59"), // Corresponding to Question "Would you recommend this product?"
                CreatedAt = new DateTime(2021, 10, 8, 12, 30, 0)
            },
            new Option
            {
                Id = Guid.Parse("5f967174-7366-409e-b33e-6b2d4bce532a"),
                Text = "No",
                QuestionId = Guid.Parse("59fe555e-3bcc-4ace-b9fc-68b76805ac59"), // Corresponding to Question "Would you recommend this product?"
                CreatedAt = new DateTime(2021, 10, 8, 12, 35, 0)
            }
        };
    }
}
