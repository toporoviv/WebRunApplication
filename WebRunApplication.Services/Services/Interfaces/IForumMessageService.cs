using WebRunApplication.Domain.Entities.Forum;

namespace WebRunApplication.Services.Interfaces
{
    public interface IForumMessageService
    {
        Task<IBaseResponse<ForumMessage>> CreateAsync(Infrastructure.Models.ForumMessage model, CancellationToken cancellationToken = default);

        Task<IBaseResponse<IEnumerable<ForumMessage>>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<IBaseResponse<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
