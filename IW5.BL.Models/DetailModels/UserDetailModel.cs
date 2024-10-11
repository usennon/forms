using IW5.BL.Models.DetailModels;

namespace IW5.BL.Models
{
    public class UserDetailModel : DetailModelBase
    {
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<FormListModel> Forms { get; set; } = [];
    }
}
