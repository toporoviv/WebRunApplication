using WebRunApplication.Domain.Entities.Forum;

namespace WebRunApplication.Services.Interfaces;

public interface IForumReactionService
{
    Task<IBaseResponse<ForumReaction>> CreateAsync
    (
        Infrastructure.Models.ForumReaction model,
        CancellationToken cancellationToken = default
    );

    Task<IBaseResponse<IEnumerable<ForumReaction>>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IBaseResponse<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}