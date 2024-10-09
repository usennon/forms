namespace IW5.BL.Models
{
    public class OptionListModel : IModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsCheked { get; set; }
    }
}
