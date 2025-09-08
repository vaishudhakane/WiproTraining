using DoConnect.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoConnect_Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.Questions).WithOne(q => q.User).HasForeignKey(q => q.UserId);
            modelBuilder.Entity<User>().HasMany(u => u.Answers).WithOne(a => a.User).HasForeignKey(a => a.UserId);
            modelBuilder.Entity<Question>().HasMany(q => q.Answers).WithOne(a => a.Question).HasForeignKey(a => a.QuestionId);
            modelBuilder.Entity<Question>().HasMany(q => q.Images).WithOne(i => i.Question).HasForeignKey(i => i.QuestionId);
            modelBuilder.Entity<Answer>().HasMany(a => a.Images).WithOne(i => i.Answer).HasForeignKey(i => i.AnswerId);
             modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

             modelBuilder.Entity<Answer>()
                .HasOne(a => a.User)
                .WithMany(u => u.Answers)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                //newly added
                modelBuilder.Entity<Question>()
        .Property(q => q.Status)
        .HasDefaultValue("Pending");

    modelBuilder.Entity<Answer>()
        .Property(a => a.Status)
        .HasDefaultValue("Pending");
        }
    }
}
