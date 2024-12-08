using System.ComponentModel.DataAnnotations;

namespace IW5.Models.Entities
{
    public interface BaseEntity
    {
        Guid Id { get; set; }
        [DataType(DataType.DateTime)]
        DateTime CreatedAt { get; set; }
    }
}
