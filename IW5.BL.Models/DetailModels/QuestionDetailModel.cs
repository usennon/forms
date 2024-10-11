using IW5.BL.Models.DetailModels;

namespace IW5.BL.Models
{
    public class QuestionDetailModel : DetailModelBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRequired { get; set; }
        public List<OptionListModel> Options { get; set; } = [];
    }
}
