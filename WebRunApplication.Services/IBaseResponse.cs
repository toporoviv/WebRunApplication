using WebRunApplication.Domain.Enums;

namespace WebRunApplication.Services
{
    public interface IBaseResponse<T>
    {
        string Description { get; }

        StatusCode StatusCode { get; }

        T Data { get; }
    }
}
