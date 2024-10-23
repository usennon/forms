namespace IW5.BL.Models.ManipulationModels.OptionModels
{
    public record OptionForManipulationDTO : IManipulationDTO
    {
        public string Text { get; set; }
        public Guid QuestionId { get; set; }
        public bool IsCheked { get; set; }
    }
}
