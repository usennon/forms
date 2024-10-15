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
    }
}
