using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GuessIt.Model;

namespace GuessIt.Data;

public class AppDbContext: IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }
    
    public DbSet<Attempt> Attempts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<UserQuiz> UserQuizzes { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Question> Questions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Attempt>(entity =>
            {
            entity.HasKey(a => a.Id);
            entity.HasOne(a => a.User)
                .WithMany(u => u.Attempts)
                .HasForeignKey(a => a.UserId);
            
            entity.HasOne(a => a.Quiz)
                .WithMany(q => q.Attempts)
                .HasForeignKey(a => a.QuizId);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.HasMany(q => q.Quizzes)
                .WithOne(c => c.Category)
                .HasForeignKey(q => q.CategoryId);
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(g => g.Id);
            entity.HasOne(g => g.Quiz)
                .WithMany(q => q.Grades)
                .HasForeignKey(g => g.QuizId);
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(q => q.Id);
            entity.HasOne(q => q.Quiz)
                .WithMany(q => q.Questions)
                .HasForeignKey(q => q.QuizId);
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(q => q.Id);
            entity.HasOne(q => q.Category)
                .WithMany(c => c.Quizzes)
                .HasForeignKey(q => q.CategoryId);
            
            entity.HasMany(q => q.Attempts)
                .WithOne(a => a.Quiz)
                .HasForeignKey(a => a.QuizId);
            
            entity.HasMany(q => q.Grades)
                .WithOne(g => g.Quiz)
                .HasForeignKey(g => g.QuizId);
            
            entity.HasMany(q => q.Questions)
                .WithOne(ques => ques.Quiz)
                .HasForeignKey(ques => ques.QuizId);
            
            entity.HasMany(q => q.UserQuizzes)
                .WithOne(uq => uq.Quiz)
                .HasForeignKey(uq => uq.QuizId);
        });

        modelBuilder.Entity<UserQuiz>(entity =>
        {
            entity.HasKey(uq => uq.Id);
            entity.HasOne(uq => uq.User)
                .WithMany(u => u.UserQuizzes)
                .HasForeignKey(uq => uq.UserId);
            
            entity.HasOne(uq => uq.Quiz)
                .WithMany(q => q.UserQuizzes)
                .HasForeignKey(uq => uq.QuizId);
            
            entity.HasIndex(uq => new { uq.QuizId, uq.UserId }).IsUnique();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasMany(u => u.Attempts)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);
            entity.HasMany(u => u.UserQuizzes)
                .WithOne(uq => uq.User)
                .HasForeignKey(uq => uq.UserId);
        });


    }

}