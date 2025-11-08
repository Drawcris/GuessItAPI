namespace GuessIt.Model;

public class UserQuiz
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public string UserId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public User User { get; set; }
    public Quiz Quiz { get; set; }
}