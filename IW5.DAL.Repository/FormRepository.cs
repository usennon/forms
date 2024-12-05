using IW5.DAL.Contracts;
using IW5.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IW5.DAL.Repository
{
    public class FormRepository : BaseRepo<Form>, IFormRepository
    {
        public FormRepository(FormsDbContext repositoryContext) : base(repositoryContext)
        {
        }
        public override IQueryable<Form> GetAll(bool trackChanges)
        {
            return base.GetAll(trackChanges).Include(f => f.Questions).Include(a=>a.Author);
        }
        public async Task<Form> GetFormByTitleAsync(string title, bool trackChanges)
            => await GetByCondition(f => f.Title.ToLower().Contains(title.ToLower()), trackChanges, f => f.Questions)
            .SingleOrDefaultAsync();

        public override async Task<Form> GetByIdAsync(Guid id, bool trackChanges)
            => await GetByCondition(e => e.Id == id, trackChanges, q => q.Questions).SingleOrDefaultAsync();
        public IQueryable<Form> SearchFormByTitle(string title, bool trackChanges)
            => GetByCondition(f => f.Title.ToLower().Contains(title.ToLower()), trackChanges, f => f.Questions);
        public async Task<IEnumerable<Form>?> GetFormByCreatedAtAsync(bool trackChanges, DateTime? start, DateTime? end)
        {
            if (start.HasValue && end.HasValue && start < end)
            {
                return await GetByCondition(f => f.CreatedAt >= start && f.CreatedAt <= end, trackChanges, f => f.Questions)
                    .ToListAsync();
            }
            else if (start.HasValue)
            {
                return await GetByCondition(f => f.CreatedAt >= start, trackChanges, f => f.Questions)
                    .ToListAsync();
            }
            else if (end.HasValue)
            {
                return await GetByCondition(f => f.CreatedAt <= end, trackChanges, f => f.Questions)
                    .ToListAsync();
            }
            else
            {
                return Enumerable.Empty<Form>();
            }
        }
        public async Task<IEnumerable<Form>?> GetActiveFormsAsync(bool trackChanges) =>
            await GetByCondition(f => f.StartDate <= DateTime.Now && f.EndDate >= DateTime.Now, trackChanges, f => f.Questions)
                .ToListAsync();


    }
}
