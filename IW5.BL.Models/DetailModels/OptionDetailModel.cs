﻿namespace IW5.BL.Models.DetailModels
{
    public class OptionDetailModel : DetailModelBase
    {
        public string Text { get; set; }
        public Guid QuestionId { get; set; }
        public bool IsCheсked { get; set; }
    }
}
