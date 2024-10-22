using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IW5.BL.Models.ManipulationModels.QuestionModels
{
    public record QuestionForManipulationDTO : IManipulationDTO
    {
        public string Text { get; init; }
        public string Description { get; init; }
        public bool IsRequired { get; init; }
    }
}
