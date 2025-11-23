using GuessIt.Data;
using GuessIt.Models;
using GuessIt.Repositories.Interfaces;


namespace GuessIt.Repositories;

public class QuizRepository : IQuizRepository
{
    private readonly AppDbContext _context;
    
    public QuizRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<IQueryable<Quiz>> GetAllQuizzes()
    {
        return Task.FromResult(_context.Quizzes.AsQueryable());
    }

    public async Task<Quiz?> GetQuizById(int id)
    {
        return await _context.Quizzes.FindAsync(id);
    }

    public async Task<Quiz> CreateQuiz(Quiz quiz)
    {
        _context.Quizzes.AddAsync(quiz);
        await _context.SaveChangesAsync();
        return quiz;
    }

    public async Task<Quiz?> UpdateQuiz(int id, Quiz quiz)
    {
        var existingQuiz = await _context.Quizzes.FindAsync(id);
        if (existingQuiz == null)
        {
            return null;
        }
        
        existingQuiz.Title = quiz.Title;
        existingQuiz.Description = quiz.Description;
        existingQuiz.ImageUrl = quiz.ImageUrl;
        await _context.SaveChangesAsync();
        return existingQuiz;
    }

    public async Task<Quiz?> DeleteQuizAsync(int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz == null)
        {
            return null;
        }

        _context.Quizzes.Remove(quiz);
        await _context.SaveChangesAsync();
        return quiz;
    }

    public async Task<IQueryable<Question>> GetQuestionsInQuiz(int quizId)
    {
        var questions =  _context.Questions.Where(x => x.QuizId == quizId).AsQueryable();
        return await Task.FromResult(questions);
    }
}