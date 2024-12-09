using IW5.Models.Entities;
using IW5.Common.Enums;

namespace IW5.BL.Models.ListModels
{
    public class QuestionListModel : ListModelBase
    {
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public bool IsRequired { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Option> Options { get; set; }
    }
}
