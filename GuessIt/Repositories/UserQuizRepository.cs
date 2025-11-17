using GuessIt.Repositories.Interfaces;
using GuessIt.Models;
using GuessIt.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GuessIt.Repositories;

public class UserQuizRepository : IUserQuizRepository
{
    private readonly AppDbContext _context;
    
    public UserQuizRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IQueryable<UserQuiz>> GetAllUserQuizesAsync()
    {
        return await Task.FromResult(_context.UserQuizzes.AsQueryable());
    }
    
    public async Task<UserQuiz?> GetUserQuizByIdAsync(int id)
    {
        return await _context.UserQuizzes.FindAsync(id);
    }

    public async Task<UserQuiz?> InviteUserToQuizAsync(UserQuiz userQuiz)
    {
        await _context.UserQuizzes.AddAsync(userQuiz);
        await _context.SaveChangesAsync();
        return userQuiz;
    }

    public async Task<UserQuiz?> DeleteUserFromQuizAsync(string userId, int quizId)
    {
        var quizToDelete = await _context.UserQuizzes.Where(x => x.QuizId == quizId 
                                                                 && x.UserId == userId).FirstOrDefaultAsync();
         _context.UserQuizzes.Remove(quizToDelete);
        await _context.SaveChangesAsync();
        return quizToDelete;
    }

    public async Task<List<UserQuiz>> MyQuizzesAsync(string userId)
    {
        var userQuizzes = _context.UserQuizzes.Where(x => x.UserId == userId).ToListAsync();
        return await userQuizzes;
    }
}