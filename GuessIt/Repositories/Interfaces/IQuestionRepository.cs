using GuessIt.Models;

namespace GuessIt.Repositories.Interfaces;

public interface IQuestionRepository
{
    public Task<IQueryable<Question>> GetallQuestionsAsync();
    public Task<Question?> GetQuestionAsync(int id);
    public Task<Question> AddQuestionAsync(Question question);
    public Task<Question?> DeleteQuestionAsync(int id);
}