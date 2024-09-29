using IW5.DAL.Contracts;
using IW5.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IW5.DAL.Repository
{
    public class OptionRepository : BaseRepo<Option>, IOptionRepository
    {
        public OptionRepository(FormsDbContext repositoryContext) : base(repositoryContext)
        {

        }
        public async Task<IEnumerable<Option>> GetOptionsFromQuestionAsync(Guid id, bool trackChanges) =>
            await GetByCondition(q => q.QuestionId.Equals(id), trackChanges, q => q.Question)
            .ToListAsync();
    }
}
