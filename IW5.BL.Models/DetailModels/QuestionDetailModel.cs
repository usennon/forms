namespace IW5.BL.Models
{
    public class QuestionDetailModel : IModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public bool IsRequired { get; set; }
        public List<OptionListModel> Options { get; set; } = [];
    }
}
