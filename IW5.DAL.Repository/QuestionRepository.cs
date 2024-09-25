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

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync(bool trackChanges) =>
           await GetAll(trackChanges)
            .ToListAsync();

        public async Task<Question> GetQuestionByIdAsync(Guid id, bool trackChanges) =>
            await GetByCondition(q => q.Id.Equals(id), trackChanges, q => q.Options).SingleOrDefaultAsync();

        public void CreateQuestionForForm(Guid FormId, Question question)
        {
            question.FormId = FormId;
            Create(question);
        }

        public void DeleteQuestion(Question question) => Delete(question);
    }
}
