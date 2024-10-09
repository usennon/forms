namespace IW5.BL.Models
{
    public class FormListModel : IModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
