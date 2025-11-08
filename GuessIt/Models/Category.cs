namespace GuessIt.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
}