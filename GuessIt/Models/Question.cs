namespace GuessIt.Models;

public enum QuestionType
{
    MultipleChoice,
    TrueFalse,
    SingleChoice,
    OpenEnded
};
public class Question
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public string QuestionContent { get; set; }
    public QuestionType QuestionType { get; set; }
    public string? QuestionImageUrl { get; set; }
    public int Points { get; set; }
    public string? CorrectAnswers { get; set; }
    public string? PossibleAnswers { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public Quiz Quiz { get; set; }
}