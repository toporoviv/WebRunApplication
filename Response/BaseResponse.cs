using WebRunApplication.Enums;
using WebRunApplication.Interfaces;

namespace WebRunApplication.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; }

        public StatusCode StatusCode { get; set; }

        public T Data { get; set; }
    }
}
