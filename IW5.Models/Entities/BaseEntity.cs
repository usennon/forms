using System.ComponentModel.DataAnnotations;

namespace IW5.Models.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
