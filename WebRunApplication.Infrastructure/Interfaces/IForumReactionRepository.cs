using WebRunApplication.Domain.Entities.Forum;

namespace WebRunApplication.Infrastructure.Interfaces;

public interface IForumReactionRepository
{
    Task<ForumReaction> CreateForumReactionAsync
    (
        Models.ForumReaction forumReaction,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<ForumReaction>> GetForumReactionsAsync(CancellationToken cancellationToken = default);

    Task<ForumReaction?> GetForumReactionByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<ForumReaction> UpdateForumReactionAsync
    (
        int id,
        Models.ForumReaction forumReaction,
        CancellationToken cancellationToken = default
    );

    Task<ForumReaction?> DeleteForumReactionAsync(int id, CancellationToken cancellationToken = default);
}