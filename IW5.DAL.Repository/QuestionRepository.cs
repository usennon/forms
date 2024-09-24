using IW5.DAL.Contracts;
using IW5.Models.Entities;
using System;


namespace IW5.DAL.Repository
{
    public class QuestionRepository : BaseRepo<Question>, IQuestionRepository
    {
        public QuestionRepository(FormsDbContext repositoryContext) : base(repositoryContext)
        {
        }
        protected override ICollection<string> NavigationPathDetail =>
            [$"{nameof(Question.Options)}"];
    }
}
