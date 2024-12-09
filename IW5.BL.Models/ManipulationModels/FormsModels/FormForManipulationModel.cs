using IW5.BL.Models.ManipulationModels.QuestionModels;

namespace IW5.BL.Models.ManipulationModels.FormsModels
{
    public record FormForManipulationModel : IManipulationModel
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid AuthorId { get; set; }
        public List<QuestionForManipulationModel>? Questions { get; set; } = new List<QuestionForManipulationModel>();
    }
    public class SubmitFormModel
    {
        public Guid UserId { get; set; }
        public Guid FormId { get; set; }
        public List<AnswerSubmission> Answers { get; set; }
    }

    public class AnswerSubmission
    {
        public Guid QuestionId { get; set; }
        public string? AnswerText { get; set; }
        public Guid? AnswerOptionId { get; set; }
    }

}
