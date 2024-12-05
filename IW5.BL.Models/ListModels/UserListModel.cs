using IW5.BL.Models.ListModels;
using IW5.Common.Enums;
using IW5.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IW5.BL.Models.ListModels
{
    public class UserListModel : ListModelBase
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

    }
}
