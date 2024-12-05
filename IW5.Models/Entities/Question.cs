using AutoMapper;
using IW5.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IW5.Models.Entities
{
    [Table("Questions", Schema = "dbo")]
    public class Question : BaseEntity
    {
        [Required]
        [MaxLength(500)]
        public string Text { get; set; }

        [Required]
        public QuestionType Type { get; set; } 

        [MaxLength(1000)]
        public string? Description { get; set; } 

        public bool IsRequired { get; set; }

        public ICollection<Option>? Options { get; set; } = new List<Option>();

        [Required]
        public Guid FormId { get; set; }

        [JsonIgnore]
        [ForeignKey("FormId")]
        public Form Form { get; set; } 


    }
}

