namespace WebRunApplication.Domain.Entities.Forum;

public class ForumReaction
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int MessageId { get; set; }

    // todo: переделать на enum
    public bool IsLike { get; set; }
}
