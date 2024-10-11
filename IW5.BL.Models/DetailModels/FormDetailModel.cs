﻿using IW5.BL.Models.DetailModels;

namespace IW5.BL.Models
{
    public class FormDetailModel : DetailModelBase
    {
        public string Title { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<QuestionListModel> Questions { get; set; } = [];

    }
}
