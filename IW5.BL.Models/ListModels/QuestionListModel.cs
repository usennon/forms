using IW5.Common.Enums;

namespace IW5.BL.Models
{
    public class QuestionListModel : IModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public bool IsRequired { get; set; }
    }
}
