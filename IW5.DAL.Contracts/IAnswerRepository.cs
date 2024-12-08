using IW5.BL.Models.ManipulationModels.FormsModels;
using IW5.Models.Entities;

namespace IW5.DAL.Contracts
{
    public interface IAnswerRepository : IRepo<Answer>
    {
        Task SaveFormAnswersAsync(SubmitFormModel model);

    }
}
