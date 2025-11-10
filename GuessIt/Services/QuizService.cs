using GuessIt.Models;
using GuessIt.Repositories;
using GuessIt.DTOs;
using System.Linq;
using GuessIt.Repositories.Interfaces;

namespace GuessIt.Services;

public class QuizService
{
    private readonly IQuizRepository _quizRepository;
    
    public QuizService(IQuizRepository quizRepository)
    {
        _quizRepository = quizRepository;
    }

    public async Task<IQueryable<Quiz>> GetAllQuizzes()
    {
        return await _quizRepository.GetAllQuizzes();
        
    }
    
    public async Task<Quiz?> GetQuizById(int id)
    {
        return await _quizRepository.GetQuizById(id);
    }

    public async Task<Quiz> CreateQuiz(CreateQuizDTO dto, string creatorId)
    {
        var quiz = new Quiz
        {
            Title = dto.Title,
            Description = dto.Description,
            CategoryId = dto.CategoryId,
            CreatorId = creatorId,
            RequiresFocusPage = dto.requiresFocusPage,
            RequiresInvite = dto.requiresInvite,
            RequiresReview = dto.requiresReview
        };
        return await _quizRepository.CreateQuiz(quiz);
    }

    public async Task<Quiz?> UpdateQuiz(int id, UpdateQuizDTO dto)
    {
        var quiz = new Quiz
        {
            Title = dto.Title,
            Description = dto.Description,
            CategoryId = dto.CategoryId,
            RequiresFocusPage = dto.RequiresFocusPage,
            RequiresInvite = dto.RequiresInvite,
            RequiresReview = dto.RequiresReview
        };
        return await _quizRepository.UpdateQuiz(id, quiz);
    }
    
    public async Task<Quiz?> DeleteQuizAsync(int id)
    {
        return await _quizRepository.DeleteQuizAsync(id);
    }
}