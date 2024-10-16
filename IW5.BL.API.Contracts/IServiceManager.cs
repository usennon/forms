using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IW5.BL.API.Contracts
{
    public interface IServiceManager
    {
        IFormBLogic FormService { get; }
        IUserBLogic UserService { get; }
        IQuestionBLogic QuestionService { get; }
        IOptionBLogic OptionService { get; }
    }
}
