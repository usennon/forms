﻿namespace IW5.BL.Models.ManipulationModels.FormsModels
{
    public record FormForManipulationModel : IManipulationModel
    {
        public string Title { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public Guid AuthorId { get; init; }
    }
}
