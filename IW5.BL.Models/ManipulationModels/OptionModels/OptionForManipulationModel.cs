﻿namespace IW5.BL.Models.ManipulationModels.OptionModels
{
    public record OptionForManipulationModel : IManipulationModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid QuestionId { get; set; }
        public bool IsChecked { get; set; }
    }
}
