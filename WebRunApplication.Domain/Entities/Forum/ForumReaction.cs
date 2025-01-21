using WebRunApplication.Domain.Enums.Forum;

namespace WebRunApplication.Domain.Entities.Forum;

public class ForumReaction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int MessageId { get; set; }
    public ReactionType Reaction { get; set; }
}
