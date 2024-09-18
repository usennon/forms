using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IW5.Models.Entities
{
    public class Form : BaseEntity
    {
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        public User Author { get; set; }

        [ForeignKey("Author")]
        public Guid AuthorId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset EndDate { get; set; }

        public IEnumerable<Question> Questions { get; set; } = new List<Question>();

        [DataType(DataType.DateTime)]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    }

    //public class FormEntityMapperProfile : Profile
    //{
    //    public FormEntityMapperProfile()
    //    {
    //        CreateMap<FormEntity, FormEntity>();
    //    }
    //}
}
