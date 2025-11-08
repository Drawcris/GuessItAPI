namespace GuessIt.Models;


public class Quiz
{
    public int Id { get;set; }
    public string Title { get;set; }
    public string Description { get;set; }
    public string ImageUrl { get;set; }
    public string CreatorId { get;set; }
    public int CategoryId { get;set; }
    public int NumberOfQuestions { get;set; }
    public bool RequiresFocusPage { get;set; }
    public bool RequiresInvite { get;set; }
    public bool RequiresReview { get;set; }
    public int MaximumPoints { get;set; }
    public int TotalSuccessfulAttempts { get;set; }
    public int TotalFailedAttempts { get;set; }
    
    public DateTime CreatedAt { get;set; }
    public DateTime UpdatedAt { get;set; }
    
    public Category Category { get;set; }
    public ICollection<UserQuiz> UserQuizzes { get;set; }
    public ICollection<Attempt> Attempts { get;set; }
    public ICollection<Question> Questions { get;set; }
    public ICollection<Grade> Grades { get;set; }
    
    public Quiz()
    {
        UserQuizzes = new List<UserQuiz>();
        Attempts = new List<Attempt>();
        Questions = new List<Question>();
        Grades = new List<Grade>();
    }
}