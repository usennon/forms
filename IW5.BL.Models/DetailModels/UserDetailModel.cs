using IW5.Common.Enums;

namespace IW5.BL.Models
{
    public class UserDetailModel : IModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<FormListModel> Forms { get; set; } = [];
    }
}
