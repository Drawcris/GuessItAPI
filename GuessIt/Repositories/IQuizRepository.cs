using GuessIt.Models;

namespace GuessIt.Repositories;

public interface IQuizRepository
{
    public Task<IQueryable<Quiz>> GetAllQuizzes();
    public Task<Quiz> GetQuizById(int id);
    public Task<Quiz> CreateQuiz(Quiz quiz);
    public Task<Quiz> UpdateQuiz(int id, Quiz quiz);
    public Task<Quiz?> DeleteQuizAsync(int id);
}