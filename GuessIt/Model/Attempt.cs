namespace GuessIt.Model;

public class Attempt
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int QuizId { get; set; }
    public int Score { get; set; }
    public bool IsSuccessful { get; set; }
    public TimeSpan TimeTaken { get; set; }
    public int Grade { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public User User { get; set; }
    public Quiz Quiz { get; set; }
}
