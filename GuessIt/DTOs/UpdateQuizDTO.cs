namespace GuessIt.DTOs;

public class UpdateQuizDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public bool RequiresFocusPage { get; set; }
    public bool RequiresInvite { get; set; }
    public bool RequiresReview { get; set; }
}