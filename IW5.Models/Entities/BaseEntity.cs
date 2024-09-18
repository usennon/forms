using IW5.DAL.Contracts;
namespace IW5.Models.Entities
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
    }
}
