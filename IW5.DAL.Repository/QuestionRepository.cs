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
    }
}
