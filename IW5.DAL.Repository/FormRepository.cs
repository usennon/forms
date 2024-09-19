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
        public async Task<IEnumerable<Form>> GetAllFormsAsync(bool trackChanges) =>
            await GetAll(trackChanges)
            .OrderBy(c => c.Title)
            .ToListAsync();

        public async Task<Form> GetByTitleAsync(string title, bool trackChanges) =>
            await GetByCondition(c => c.Title.Equals(title), trackChanges)
            .SingleOrDefaultAsync();

    }
}
