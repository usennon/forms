using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IW5.DAL.Contracts
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }
        IFormRepository Form { get; }
        IQuestionRepository Question { get; }
        IOptionRepository Option { get; }

        Task SaveAsync();
    }
}
