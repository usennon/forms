using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;

namespace IW5.BL.Models.DetailModels
{
    public class QuestionDetailModel : DetailModelBase
    {
        public string Text { get; set; }
        public string Description { get; set; }
        public Guid FormId { get; set; }
        public bool IsRequired { get; set; }
        public List<OptionListModel> Options { get; set; } = [];
    }
}
