using IW5.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IW5.API.DAL
{
    public class FormsDbContext : DbContext
    {
        public DbSet<FormEntity> Forms { get; set; } = null!;
        public DbSet<QuestionEntity> Questions { get; set; } = null!;
        public DbSet<UserEntity> Users { get; set; } = null!;

        public FormsDbContext(DbContextOptions<FormsDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FormEntity>()
                .HasMany(formEntity => formEntity.Questions)
                .WithOne(questionEntity => questionEntity.Form)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserEntity>()
                .HasMany(typeof(FormEntity))
                .WithOne(nameof(FormEntity.Author))
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
