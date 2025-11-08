namespace GuessIt.Model;

public class Grade
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public string GradeValue { get; set; }
    public int MinScore { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public Quiz Quiz { get; set; }
}