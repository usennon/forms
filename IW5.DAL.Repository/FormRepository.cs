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
        protected override ICollection<string> NavigationPathDetail =>
            [$"{nameof(Form.Questions)}"];
        public override async Task<Form> GetById(Guid id)
        {
            var query = context.Forms.AsQueryable();
            foreach (var detail in NavigationPathDetail)
            {
                query = query.Include(detail);
            }
            return await query.SingleOrDefaultAsync(entity => entity.Id == id);
        }
        public async Task<IEnumerable<Form>> GetAllFormsAsync(bool trackChanges) =>
            await GetAll(trackChanges)
            .OrderBy(c => c.Title)
            .ToListAsync();

    }
}
