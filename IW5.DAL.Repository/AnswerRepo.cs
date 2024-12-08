using IW5.BL.Models.ManipulationModels.FormsModels;
using IW5.DAL.Contracts;
using Microsoft.EntityFrameworkCore;

namespace IW5.DAL.Repository
{
    public class AnswerRepository : BaseRepo<Answer>, IAnswerRepository
    {
        private FormsDbContext _repositoryContext;
        public AnswerRepository(FormsDbContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        override public IQueryable<Answer> GetAll(bool trackChanges)
        {
            return base.GetAll(trackChanges)
                .Include(a=>a.User)
                .Include(a => a.Form)
                .Include(a => a.Question);
        }
        public override Task<Answer> GetByIdAsync(Guid id, bool trackChanges)
        {
            var query = trackChanges
                ? _repositoryContext.Answers.Include(a => a.Question).Include(b=>b.User).Include(c=>c.Form).Include(b => b.AnswerOption) // Include related Question entity (adjust the navigation property)
                : _repositoryContext.Answers.Include(a => a.Question).Include(b => b.User).Include(c => c.Form).Include(b => b.AnswerOption).AsNoTracking(); // If no tracking, use AsNoTracking() for better performance

            return query.FirstOrDefaultAsync(a => a.Id == id); // Fetch the Answer by id
        }
        public override void Delete(Answer entity)
        {
            base.Delete(entity);
            _repositoryContext.SaveChanges();

        }
        public async Task SaveFormAnswersAsync(SubmitFormModel model)
        {
            var answers = new List<Answer>();

            foreach (var answerSubmission in model.Answers)
            {
                var answer = new Answer
                {
                    UserId = model.UserId,
                    FormId = model.FormId,
                    QuestionId = answerSubmission.QuestionId,
                    AnswerText = answerSubmission.AnswerText,
                    AnswerOptionId = answerSubmission.AnswerOptionId,
                    CreatedAt = DateTime.Now
                };

                answers.Add(answer);
            }
            base.CreateRange(answers);
        }

    }
}
