using WebRunApplication.Domain.Entities;

namespace WebRunApplication.Services.Services.Interfaces
{
    public interface IUserService
    {
        // todo: понюхать этот сервис. Довольно странный, делает обертку над репозиторием
        Task<IBaseResponse<User>> CreateAsync(User model, CancellationToken cancellationToken = default);
        Task<IBaseResponse<IEnumerable<User>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IBaseResponse<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}

