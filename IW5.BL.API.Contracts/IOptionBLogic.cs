using IW5.BL.Models.ListModels;
using IW5.BL.Models.DetailModels;
using IW5.Models.Entities;
using IW5.BL.Models.ManipulationModels.OptionModels;

namespace IW5.BL.API.Contracts
{
    public interface IOptionBLogic : IBLogic<Option, OptionListModel, OptionDetailModel, OptionForManipulationModel>
    {
    }
}
