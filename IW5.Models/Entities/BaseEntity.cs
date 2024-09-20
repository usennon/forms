using IW5.Models.Interface;

namespace IW5.Models.Entities
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
    }
}
