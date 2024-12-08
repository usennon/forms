using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IW5.Models.Entities
{
    [Table("Options", Schema = "dbo")]
    public class Option: BaseEntity
    {
        public Guid Id { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        [MaxLength(250)]
        public string Text { get; set; }

        [ForeignKey("Question")]
        public Guid QuestionId { get; set; }
        [Required]
        public Question Question { get; set; }

    }
}
