using System.Security.Claims;
using WebRunApplication.Domain.Enums.Models;
using WebRunApplication.Domain.Enums.Response;
using WebRunApplication.Domain.Enums.Interfaces;

namespace WebRunApplication.Domain.Enums.Services.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Login(AuthorizationModel model);

        Task<BaseResponse<ClaimsIdentity>> Register(RegisterModel model);
    }
}
