using AutoMapper;
using IW5.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IW5.Models.Entities
{

    [Table("Users", Schema = "dbo")]
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(500)]
        [Url]
        public string? PhotoUrl { get; set; }

        [Required]
        public Role Role { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        [Phone]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
        [JsonIgnore]

        public virtual ICollection<Form> Forms { get; set; } = new List<Form>();

    }
}