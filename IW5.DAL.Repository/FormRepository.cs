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
        public async Task<Form> GetFormById(Guid Id, bool trackChanges) 
        {
            return await GetByCondition(f => f.Id.Equals(Id), trackChanges, f => f.Questions).SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<Form>> GetAllFormsAsync(bool trackChanges) =>
            await GetAll(trackChanges)
            .OrderBy(c => c.Title)
            .ToListAsync();

    }
}
