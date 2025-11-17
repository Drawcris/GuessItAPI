using GuessIt.Repositories.Interfaces;
using GuessIt.Models;
using GuessIt.DTOs;

namespace GuessIt.Services;

public class UserQuizService
{
    private readonly IUserQuizRepository _userQuizRepository;
    
    public UserQuizService(IUserQuizRepository userQuizRepository)
    {
        _userQuizRepository = userQuizRepository;
    }

    public async Task<IQueryable<UserQuiz>> GetAllUserQuizzesAsync()
    {
        return await _userQuizRepository.GetAllUserQuizesAsync();
    }
    
    public async Task<UserQuiz?> GetUserQuizByIdAsync(int id)
    {
        return await _userQuizRepository.GetUserQuizByIdAsync(id);
    }

    public async Task<UserQuiz?> InviteUserToQuizAsync(InviteToQuizDTO inviteToQuiz)
    {
        var newUserQuiz = new UserQuiz()
        {
            QuizId = inviteToQuiz.QuizId,
            UserId = inviteToQuiz.UserId
        };
        
        return await _userQuizRepository.InviteUserToQuizAsync(newUserQuiz);
    }

    public async Task<UserQuiz?> DeleteUserFromQuizAsync(DeleteUserFromQuizDTO dto)
    {
        var userQuiz = await _userQuizRepository.DeleteUserFromQuizAsync(dto.UserId, dto.QuizId);
        return userQuiz;
    }
    
    public async Task<List<UserQuiz>> MyQuizzesAsync(string userId)
    {
        return await _userQuizRepository.MyQuizzesAsync(userId);
    }
}