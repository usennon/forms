﻿using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using IW5.BL.Models.ManipulationModels.FormsModels;
using IW5.Common.Enums.Sorts;
using IW5.Models.Entities;

namespace IW5.BL.API.Contracts
{
    public interface IFormBLogic : IBLogic<Form, FormListModel, FormDetailModel, FormForManipulationModel>
    {
        IEnumerable<FormListModel> GetFilteredForms(string substring, FormSortType type);
        Task SaveFormAnswersAsync(SubmitFormModel submitFormModel);
        Task<FormDetailModel> GetFormByTitleAsync(string title);
    }
}
