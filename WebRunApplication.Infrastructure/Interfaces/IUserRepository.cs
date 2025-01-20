using WebRunApplication.Domain.Entities;

namespace WebRunApplication.Infrastructure.Interfaces;

public interface IUserRepository
{
    Task<User> CreateUserAsync(Models.User user, CancellationToken cancellationToken = default);
    Task<User> UpdateUserAsync(int userId, Models.User user, CancellationToken cancellationToken = default);
    Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken = default);
    Task<User?> DeleteUserByIdAsync(int id, CancellationToken cancellationToken = default);
}