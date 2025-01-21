using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebRunApplication.Domain.Entities;
using WebRunApplication.Domain.Enums;
using WebRunApplication.Infrastructure.Interfaces;
using WebRunApplication.Services.Extensions;
using WebRunApplication.Services.Interfaces;
using WebRunApplication.Services.Models;

namespace WebRunApplication.Services.Implementations
{
    public class AccountService
    (
        ILogger<AccountService> logger,
        IUserRepository userRepository
    ) : IAccountService
    {
        public async Task<BaseResponse<ClaimsIdentity>> LoginAsync
        (
            AuthorizationModel model,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var user = (await userRepository.GetUsersAsync(cancellationToken))
                    .FirstOrDefault(user => user.Login == model.Login);
                
                if (user is null)
                {
                    return new BaseResponse<ClaimsIdentity>
                    {
                        Description = "Пользователь не найден"
                    };
                }

                if (user.Password != model.Password)
                {
                    return new BaseResponse<ClaimsIdentity>
                    {
                        Description = "Введен неверный пароль"
                    };
                }

                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception exception)
            {
                logger.LogError(exception, $"[AccountService]: {exception.Message}");

                return new BaseResponse<ClaimsIdentity>
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> RegisterAsync
        (
            RegisterModel model,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var user = (await userRepository.GetUsersAsync(cancellationToken))
                    .FirstOrDefault(user => user.Login == model.Login);

                if (user is not null)
                {
                    return new BaseResponse<ClaimsIdentity>
                    {
                        Description = "Данный логин занят",
                        StatusCode = StatusCode.AlreadyExists
                    };
                }

                user = await userRepository.CreateUserAsync(model.ToUserWithoutId(), cancellationToken);

                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>
                {
                    Data = result,
                    Description = "Пользователь зарегистрирован",
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception exception)
            {
                logger.LogError(exception, $"[AccountService]: {exception.Message}");

                return new BaseResponse<ClaimsIdentity>
                {
                    Description = exception.Message,
                    StatusCode = Domain.Enums.StatusCode.InternalServerError
                };
            }
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new (ClaimsIdentity.DefaultNameClaimType, user.Login),
                new (ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };

            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
