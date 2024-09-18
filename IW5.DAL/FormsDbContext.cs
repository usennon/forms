using IW5.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IW5.API.DAL
{
    public class FormsDbContext : DbContext
    {
        public DbSet<Form> Forms { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public FormsDbContext(DbContextOptions<FormsDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(user => user.Forms)
                .WithOne(form => form.Author)
                .HasForeignKey(form => form.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Form>()
                .HasMany(form => form.Questions)
                .WithOne(question => question.Form)
                .HasForeignKey(question => question.FormId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasMany(question => question.Options)
                .WithOne(option => option.Question)
                .HasForeignKey(option => option.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasIndex(user => user.Email)
                .IsUnique();
        }
    }
}
