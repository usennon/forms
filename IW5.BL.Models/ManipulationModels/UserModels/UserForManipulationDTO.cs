﻿using IW5.Common.Enums;

namespace IW5.BL.Models.ManipulationModels.UserModels
{
    public record UserForManipulationDTO : IManipulationDTO
    {
        public string Name { get; init; }
        public string PhotoUrl { get; init; }
        public string Email { get; init; }
        public Role Role { get; init; }
    }
}
