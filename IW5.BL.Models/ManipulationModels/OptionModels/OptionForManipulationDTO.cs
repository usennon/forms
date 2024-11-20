namespace IW5.BL.Models.ManipulationModels.OptionModels
{
    public record OptionForManipulationDTO : IManipulationModel
    {
        public string Text { get; set; }
        public Guid QuestionId { get; set; }
        public bool IsChecked { get; set; }
    }
}
