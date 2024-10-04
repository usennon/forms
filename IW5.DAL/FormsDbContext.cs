using IW5.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IW5.DAL
{
    public class FormsDbContext : DbContext
    {
        public DbSet<Form> Forms { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Option> Options { get; set; } = null!;

        public FormsDbContext(DbContextOptions<FormsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureUserEntity(modelBuilder);
            ConfigureFormEntity(modelBuilder);
            ConfigureQuestionEntity(modelBuilder);
            ConfigureOptionEntity(modelBuilder);
        }

        private void ConfigureUserEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(user => user.Forms)
                .WithOne(form => form.Author)
                .HasForeignKey(form => form.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasIndex(user => user.Email)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(user => user.PhoneNumber)
                .IsUnique();
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("GETDATE()");
            });
        }

        private void ConfigureFormEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Form>()
                .HasMany(form => form.Questions)
                .WithOne(question => question.Form)
                .HasForeignKey(question => question.FormId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureQuestionEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasMany(question => question.Options)
                .WithOne(option => option.Question)
                .HasForeignKey(option => option.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureOptionEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Option>(entity =>
            {
                entity.Property(o => o.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(o => o.Question)
                    .WithMany(q => q.Options)
                    .HasForeignKey(o => o.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
