using GuessIt.Models;
using GuessIt.Repositories;
using GuessIt.DTOs;
using System.Linq;
using GuessIt.Repositories.Interfaces;

namespace GuessIt.Services;

public class QuizService
{
    private readonly IQuizRepository _quizRepository;
    private readonly UploadFileService _uploadFileService;
    private readonly IUserQuizRepository _userQuizRepository;
    
    public QuizService(IQuizRepository quizRepository, UploadFileService uploadFileService, 
        IUserQuizRepository userQuizRepository)
    {
        _quizRepository = quizRepository;
        _uploadFileService = uploadFileService;
        _userQuizRepository = userQuizRepository;
    }

    public async Task<IQueryable<Quiz>> GetAllQuizzes()
    {
        return await _quizRepository.GetAllQuizzes();
        
    }
    
    public async Task<Quiz?> GetQuizById(int id)
    {
        return await _quizRepository.GetQuizById(id);
    }

    public async Task<Quiz> CreateQuiz(CreateQuizDTO dto, string creatorId, IFormFile? imageFile)
    {
        var imageUrl = "/Uploads/" + _uploadFileService.UploadImageAsync(imageFile);
        var quiz = new Quiz
        {
            Title = dto.Title,
            Description = dto.Description,
            CategoryId = dto.CategoryId,
            CreatorId = creatorId,
            RequiresFocusPage = dto.requiresFocusPage,
            RequiresInvite = dto.requiresInvite,
            RequiresReview = dto.requiresReview,
            ImageUrl = imageUrl
        };
        
        var result= await _quizRepository.CreateQuiz(quiz);
        
        var userQuiz = new UserQuiz()
        {
            QuizId = result.Id,
            UserId = creatorId
        };

        await _userQuizRepository.InviteUserToQuizAsync(userQuiz);
        return result;
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
    
    public async Task<IQueryable<Question>> GetQuestionsInQuiz(int quizId)
    {
        return await _quizRepository.GetQuestionsInQuiz(quizId);
    }
}