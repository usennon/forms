using IW5.DAL.Contracts;
using IW5.Models.Entities;

namespace IW5.DAL.Repository
{
    public class OptionRepository : BaseRepo<Option>, IOptionRepository
    {
        public OptionRepository(FormsDbContext repositoryContext) : base(repositoryContext)
        {
        }

    }
}
