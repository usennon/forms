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
        public async Task<IEnumerable<Form>?> GetFormByTitleAsync(string title, bool trackChanges) 
            => await GetByCondition(f => f.Title.ToLower().Contains(title.ToLower()), trackChanges, f => f.Questions)
            .ToListAsync();
        public async Task<IEnumerable<Form>?> GetFormByCreatedAt(bool trackChanges, DateTime? start, DateTime? end)
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
        public async Task<IEnumerable<Form>?> GetActiveForms(bool trackChanges) =>
            await GetByCondition(f => f.StartDate <= DateTime.Now && f.EndDate >= DateTime.Now, trackChanges, f => f.Questions)
                .ToListAsync();
        

    }
}
