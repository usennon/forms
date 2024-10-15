using IW5.Common.Enums;
using IW5.BL.Models.ListModels;

namespace IW5.BL.Models.DetailModels
{
    public class UserDetailModel : DetailModelBase
    {
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<FormListModel> Forms { get; set; } = [];
    }
}
