using System.Security.Claims;
using WebRunApplication.Services.Models;

namespace WebRunApplication.Services.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> LoginAsync
        (
            AuthorizationModel model,
            CancellationToken cancellationToken = default
        );

        Task<BaseResponse<ClaimsIdentity>> RegisterAsync
        (
            RegisterModel model,
            CancellationToken cancellationToken = default
        );
    }
}
