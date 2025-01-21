using WebRunApplication.Domain.Enums.Forum;

namespace WebRunApplication.Infrastructure.Models;

public sealed record ForumReaction
{
    public required int UserId { get; set; }
    public required int MessageId { get; set; }
    public required ReactionType Reaction { get; set; }
}