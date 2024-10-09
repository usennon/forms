namespace IW5.BL.Models
{
    public class UserListModel : IModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
    }
}
