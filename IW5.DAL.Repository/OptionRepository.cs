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
        public async Task<IEnumerable<Option>> GetAllOptionsAsync(bool trackChanges) =>
            await GetAll(trackChanges)
            .ToListAsync();
        public async Task<IEnumerable<Option>> GetOptionsFromQuestionAsync(Guid id, bool trackChanges) =>
            await GetByCondition(q => q.QuestionId.Equals(id), trackChanges, q => q.Question)
            .ToListAsync();
        public async Task<Option> GetOptionByIdAsync(Guid optionId, bool trackChanges)
        {
            return await GetByCondition(u => u.Id.Equals(optionId), trackChanges, u => u.Question)
                .SingleOrDefaultAsync();
        }
        public void CreateOptionForQuestion(Guid questionId, Option option)
        {
            option.QuestionId = questionId;
            Create(option);
        }
        public void CreateListOfOptionsForQuestion(Guid questionId, List<Option> options)
        {
            foreach (var option in options)
            {
                option.QuestionId = questionId;
                Create(option);
            }
        }
        public void DeleteOption(Option option) => Delete(option);

    }
}
