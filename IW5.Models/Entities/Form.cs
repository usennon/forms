﻿using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IW5.Models.Entities
{
    [Table("Forms", Schema = "dbo")]
    public class Form : BaseEntity
    {
        public Guid Id { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
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
        public DateTime EndDate { get; set; }

        public ICollection<Question> Questions { get; set; } = new List<Question>();

    }

}
