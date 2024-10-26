using IW5.BL.Models.ListModels;
using IW5.Common.Enums;

namespace IW5.BL.Models.ListModels
{
    public class QuestionListModel : ListModelBase
    {
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public bool IsRequired { get; set; }
    }
}
