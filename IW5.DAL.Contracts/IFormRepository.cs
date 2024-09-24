using IW5.Models.Entities;

namespace IW5.DAL.Contracts
{
    public interface IFormRepository : IRepo<Form>
    {
        Task<IEnumerable<Form>> GetAllFormsAsync(bool trackChanges);
        Task<Form> GetFormByIdAsync(Guid Id, bool trackChanges);
        void CreateFormForAuthor(Guid authorId, Form form);
        void DeleteForm(Form form);


    }
}
