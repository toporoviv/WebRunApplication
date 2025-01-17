using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebRunApplication.Domain.Enums.DAL.Interfaces;
using WebRunApplication.Domain.Entities;
using WebRunApplication.Domain.Enums.Models;
using WebRunApplication.Domain.Enums.Response;
using WebRunApplication.Domain.Enums.Services.Interfaces;

namespace WebRunApplication.Domain.Enums.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly ILogger<AccountService> _logger;

        public AccountService(ILogger<AccountService> logger, IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(AuthorizationModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == model.Login);
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
                _logger.LogError(exception, $"[AccountService]: {exception.Message}");

                return new BaseResponse<ClaimsIdentity>
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == model.Login);

                if (user is not null)
                {
                    return new BaseResponse<ClaimsIdentity>
                    {
                        Description = "Данный логин занят",
                        StatusCode = StatusCode.AlreadyExists
                    };
                }

                user = new User
                {
                    Login = model.Login,
                    Password = model.Password,
                    Age = model.Age,
                    Weight = model.Weight,
                    Height = model.Height,
                    Gender = model.Gender,
                    Role = Role.User,
                    Email = model.Email,
                    Fullname = model.Fullname
                };

                await _userRepository.Create(user);

                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>
                {
                    Data = result,
                    Description = "Пользователь зарегистрирован",
                    StatusCode = Domain.Enums.StatusCode.OK
                };
            }
            catch(Exception exception)
            {
                _logger.LogError(exception, $"[AccountService]: {exception.Message}");

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
