using IW5.Common.Enums;

namespace IW5.BL.Models.ManipulationModels.UserModels
{
    public record UserForManipulationModel : IManipulationModel
    {
        public string UserName { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public string Subject { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
