using GuessIt.DTOs;
using GuessIt.Repositories.Interfaces;
using GuessIt.Models;

namespace GuessIt.Services;

public class QuestionService
{
    private readonly IQuestionRepository _questionRepository;
    
    public QuestionService(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }
    
    public async Task<IQueryable<Question>> GetAllQuestionsAsync()
    {
        return await _questionRepository.GetallQuestionsAsync();
    }
    
    public async Task<Question?> GetQuestionByIdAsync(int id)
    {
        return await _questionRepository.GetQuestionAsync(id);
    }

    public async Task<Question?> DeleteQuestionAsync(int id)
    {
        return await _questionRepository.DeleteQuestionAsync(id);
    }

    public async Task<Question> AddQuestionAsync(AddQuestionDTO dto)
    {
        var newQuestion = new Question()
        {
            QuizId = dto.QuizId,
            QuestionContent = dto.QuestionContent,
            QuestionType = dto.QuestionType,
            QuestionImageUrl = dto.QuestionImageUrl,
            Points = dto.Points,
            CorrectAnswers = dto.CorrectAnswers,
            PossibleAnswers = dto.PossibleAnswers,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        return await _questionRepository.AddQuestionAsync(newQuestion);
    }
}