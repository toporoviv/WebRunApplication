using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebRunApplication.Domain.Entities;
using WebRunApplication.Domain.Enums;
using WebRunApplication.Infrastructure.Interfaces;
using WebRunApplication.Services.Services.Interfaces;

namespace WebRunApplication.Services.Services.Implementations
{
    internal class UserService
    (
        ILogger<UserService> logger,
        IUserRepository userRepository
    ) : IUserService
    {
        public async Task<IBaseResponse<User>> CreateAsync(User model, CancellationToken cancellationToken)
        {
            try
            {
                var user = (await userRepository.GetUsersAsync(cancellationToken))
                    .FirstOrDefault(x => x.Login == model.Login);
                
                if (user is not null)
                {
                    return new BaseResponse<User>()
                    {
                        Description = "Пользователь с таким логином уже есть",
                        StatusCode = StatusCode.AlreadyExists
                    };
                }

                await userRepository.CreateUserAsync(new Infrastructure.Models.User
                {
                    Age = model.Age,
                    Email = model.Email,
                    Fullname = model.Fullname,
                    Gender = model.Gender,
                    Height = model.Height,
                    Login = model.Login,
                    Password = model.Password,
                    Weight = model.Weight,
                    Role = model.Role
                }, cancellationToken);

                return new BaseResponse<User>()
                {
                    Data = model,
                    Description = "Пользователь добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"[{nameof(UserService)}.{nameof(CreateAsync)}] error: {ex.Message}");
                return new BaseResponse<User>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<User>>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var users = (await userRepository.GetUsersAsync(cancellationToken)).ToList();

                logger.LogInformation($"[{nameof(UserService)}.{nameof(GetAllAsync)}] получено элементов {users.Count}");
                return new BaseResponse<IEnumerable<User>>()
                {
                    Data = users,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"[{nameof(UserService)}.{nameof(GetAllAsync)}] error: {ex.Message}");
                return new BaseResponse<IEnumerable<User>>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var user = (await userRepository.GetUsersAsync(cancellationToken))
                    .FirstOrDefault(user => user.Id == id);
                
                if (user is null)
                {
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.NotFound,
                        Data = false
                    };
                }

                await userRepository.DeleteUserByIdAsync(user.Id, cancellationToken);
                logger.LogInformation($"[{nameof(UserService)}.{nameof(DeleteAsync)}] пользователь удален");

                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"[{nameof(UserService)}.{nameof(DeleteAsync)}] error: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
