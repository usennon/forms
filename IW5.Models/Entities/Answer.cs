using IW5.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Answer : BaseEntity
{
    public Guid Id { get; set; }
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid UserId { get; set; }
    public User User { get; set; }
    [ForeignKey("Form")]
    public Guid FormId { get; set; }
    public Form Form { get; set; }
    [ForeignKey("Question")]
    public Guid QuestionId { get; set; }
    public Question Question { get; set; }

    public string AnswerText { get; set; }
    [ForeignKey("AnswerOption")]
    public Guid? AnswerOptionId { get; set; }

    public Option AnswerOption { get; set; }
}
