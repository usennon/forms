using AutoMapper;
using IW5.Common.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IW5.Models.Entities
{

    [Table("Users", Schema = "dbo")]
    public class User : IdentityUser<Guid>, BaseEntity
    {
        public override Guid Id { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        [MaxLength(150)]
        public override string UserName { get; set; }

        [MaxLength(500)]
        [Url]
        public string? PhotoUrl { get; set; }

        [Required]
        public Role Role { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public override string Email { get; set; }

        [Phone]
        [MaxLength(20)]
        public override string? PhoneNumber { get; set; }
        [JsonIgnore]
        public virtual ICollection<Form> Forms { get; set; } = new List<Form>();
        public bool Active { get; set; }
        public string Subject { get; set; }
    }
}