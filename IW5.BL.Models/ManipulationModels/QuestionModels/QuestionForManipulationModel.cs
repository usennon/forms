using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IW5.BL.Models.ManipulationModels.QuestionModels
{
    public record QuestionForManipulationModel : IManipulationModel
    {
        public string Text { get; init; }
        public string Description { get; init; }
        public Guid FormId { get; init; }
        public bool IsRequired { get; init; }
    }
}
