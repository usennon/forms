﻿using IW5.BL.Models.ListModels;

namespace IW5.BL.Models.ListModels
{
    public class FormListModel : ListModelBase
    {
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}