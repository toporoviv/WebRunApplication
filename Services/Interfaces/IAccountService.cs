using System.Security.Claims;
using WebRunApplication.Interfaces;
using WebRunApplication.Models;
using WebRunApplication.Response;

namespace WebRunApplication.Services.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Login(AuthorizationModel model);

        Task<BaseResponse<ClaimsIdentity>> Register(RegisterModel model);
    }
}
