using Microsoft.AspNetCore.Identity;

namespace GuessIt.Models;

public enum Role
{
    Teacher,
    Student
}

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Role RoleType { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public ICollection<Attempt> Attempts { get; set; }
    public ICollection<UserQuiz> UserQuizzes { get; set; }

    public User()
    {
        Attempts = new List<Attempt>();
        UserQuizzes = new List<UserQuiz>();
    }
}