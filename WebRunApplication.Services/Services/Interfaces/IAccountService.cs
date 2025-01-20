using System.Security.Claims;
using WebRunApplication.Services.Models;

namespace WebRunApplication.Services.Services.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Login(AuthorizationModel model);

        Task<BaseResponse<ClaimsIdentity>> Register(RegisterModel model);
    }
}
