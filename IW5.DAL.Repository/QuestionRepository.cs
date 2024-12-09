using IW5.DAL.Contracts;
using IW5.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;


namespace IW5.DAL.Repository
{
    public class QuestionRepository : BaseRepo<Question>, IQuestionRepository
    {
        public QuestionRepository(FormsDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsFromFormAsync(bool trackChanges, Guid formId) =>
           await GetByCondition(e => e.FormId.Equals(formId), false)
            .ToListAsync();

        public async Task<Question> GetQuestionByIdAsync(Guid id, bool trackChanges)
            => await GetByCondition(f => f.Id.Equals(id), trackChanges, q => q.Options)
            .SingleOrDefaultAsync();
        public IQueryable<Question> SearchQuestionByText(string text, bool trackChanges)
            => GetByCondition(f => f.Text.ToLower().Contains(text.ToLower()), trackChanges, f => f.Options);
        public override async Task<Question> GetByIdAsync(Guid id, bool trackChanges)
            => await GetByCondition(e => e.Id == id, trackChanges, q => q.Options).SingleOrDefaultAsync();
    }
}
