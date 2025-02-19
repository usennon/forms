﻿using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ListModels;
using IW5.BL.Models.ManipulationModels.UserModels;
using IW5.Common.Enums.Sorts;
using IW5.Models.Entities;

namespace IW5.BL.API.Contracts
{
    public interface IUserBLogic : IBLogic<User, UserListModel, UserDetailModel, UserForManipulationModel>
    {
        IEnumerable<UserListModel> GetFilteredUsers(string substring, UserSortType type);
    }
}
