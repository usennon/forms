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
            modelBuilder.Entity<User>()
                .Property(user => user.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");
        }

        private void ConfigureFormEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Form>()
                .HasMany(form => form.Questions)
                .WithOne(question => question.Form)
                .HasForeignKey(question => question.FormId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Form>()
                .Property(form => form.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Form>()
                .Property(form => form.StartDate)
                .HasColumnType("datetime2");
            modelBuilder.Entity<Form>()
                .Property(form => form.EndDate)
                .HasColumnType("datetime2");
        }

        private void ConfigureQuestionEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasMany(question => question.Options)
                .WithOne(option => option.Question)
                .HasForeignKey(option => option.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Question>()
                .Property(question => question.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");
        }

        private void ConfigureOptionEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Option>(entity =>
            {
                entity.HasOne(o => o.Question)
                    .WithMany(q => q.Options)
                    .HasForeignKey(o => o.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Option>()
                .Property(option => option.CreatedAt)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
