namespace GuessIt.DTOs;

public class CreateQuizDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public bool requiresFocusPage { get; set; }
    public bool requiresInvite { get; set; }
    public bool requiresReview { get; set; }
}