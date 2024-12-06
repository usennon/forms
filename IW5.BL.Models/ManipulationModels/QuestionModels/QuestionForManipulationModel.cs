using IW5.BL.Models.ManipulationModels.OptionModels;
using IW5.Common.Enums;

namespace IW5.BL.Models.ManipulationModels.QuestionModels
{
    public record QuestionForManipulationModel : IManipulationModel
    {
        public string Text { get; set; }
        public string Description { get; set; }
        public QuestionType Type { get; set; }
        public Guid FormId { get; set; }
        public bool IsRequired { get; set; }
        public List<OptionForManipulationModel>? Options { get; set; } = new List<OptionForManipulationModel>();
    }
}
