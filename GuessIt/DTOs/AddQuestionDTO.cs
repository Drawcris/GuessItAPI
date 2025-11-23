using GuessIt.Models;

namespace GuessIt.DTOs;

public class AddQuestionDTO
{
    public int QuizId { get; set; }
    public string QuestionContent { get; set; }
    public QuestionType QuestionType { get; set; }
    public string? QuestionImageUrl { get; set; }
    public int Points { get; set; }
    public string? CorrectAnswers { get; set; }
    public string? PossibleAnswers { get; set; }
}