using GuessIt.Models;

namespace GuessIt.Repositories.Interfaces;

public interface IUserQuizRepository
{
    public Task<IQueryable<UserQuiz>> GetAllUserQuizesAsync();
    public Task<UserQuiz?> GetUserQuizByIdAsync(int id);
    public Task<UserQuiz?> InviteUserToQuizAsync(UserQuiz userQuiz);
    public Task<UserQuiz?> DeleteUserFromQuizAsync(string userId, int quizId);
    public Task<List<UserQuiz>> MyQuizzesAsync(string userId);
}