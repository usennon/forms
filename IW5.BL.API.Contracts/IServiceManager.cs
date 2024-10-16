using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using IW5.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IW5.BL.API.Contracts
{
    public class IServiceManager
    {
        IFormBLogic FormService { get; }
        IUserBLogic UserService { get; }
        IUserBLogic QuestionService { get; }
        IBLogic<TEntity, TListModel, TDetailModel> GetService<TEntity, TListModel, TDetailModel>()
            where TEntity : BaseEntity
            where TListModel : ListModelBase
            where TDetailModel : DetailModelBase;
    }
}
