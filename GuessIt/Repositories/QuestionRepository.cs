using GuessIt.Repositories.Interfaces;
using GuessIt.Data;
using GuessIt.Models;

namespace GuessIt.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly AppDbContext _context;
    
    public QuestionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IQueryable<Question>> GetallQuestionsAsync()
    {
        var questions = _context.Questions.AsQueryable();
        return await Task.FromResult(questions);
    }
    
    public async Task<Question?> GetQuestionAsync(int id)
    {
        return await _context.Questions.FindAsync(id);
    }

    public async Task<Question> AddQuestionAsync(Question question)
    {
        var quiz = await _context.Quizzes.FindAsync(question.QuizId);
        if (quiz == null)
        {
            throw new Exception("Quiz not found");
        }
        quiz.NumberOfQuestions ++;
        await _context.Questions.AddAsync(question);
        await _context.SaveChangesAsync();
        return question;
    }

    public async Task<Question?> DeleteQuestionAsync(int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz != null)
        {
            quiz.NumberOfQuestions --;
        }
        var questionToDelete = await _context.Questions.FindAsync(id);
        if (questionToDelete == null)
        {
            return null;
        }
        _context.Questions.Remove(questionToDelete);
        await _context.SaveChangesAsync();
        return questionToDelete;
    }
}