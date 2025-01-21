using WebRunApplication.Domain.Entities.Forum;

namespace WebRunApplication.Infrastructure.Interfaces;

public interface IForumMessageRepository
{
    Task<ForumMessage> CreateForumMessageAsync
    (
        Models.ForumMessage forumMessage,
        CancellationToken cancellationToken = default
    );

    Task<ForumMessage?> GetForumMessageByIdAsync(int id, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<ForumMessage>> GetForumMessages(CancellationToken cancellationToken = default);
    
    Task<ForumMessage> UpdateForumMessageAsync
    (
        int id,
        Models.ForumMessage forumMessage,
        CancellationToken cancellationToken = default
    );

    Task<ForumMessage?> DeleteForumMessageAsync(int id, CancellationToken cancellationToken = default);
}