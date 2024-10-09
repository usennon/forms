using IW5.BL.Models;
using IW5.Common.Enums;
using IW5.Common.Enums.Sorts;
using IW5.Models.Entities;

namespace IW5.BL.API.Contracts
{
    public interface IUserBLogic : IBLogic<User, UserListModel, UserDetailModel>
    {
        IQueryable<User> SearchUsers(string substring);
        IQueryable<User> SortUsers(IQueryable<User> searchQuery, UserSortType type);
        Task<IEnumerable<UserListModel>> GetFilteredUsers(string substring, UserSortType type);
    }
}
