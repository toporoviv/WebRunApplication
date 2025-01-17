using WebRunApplication.Domain.Enums.Interfaces;

namespace WebRunApplication.Domain.Enums.Services.Interfaces
{
    public interface IMailSenderService
    {
        Task<IBaseResponse<bool>> SendMessage(uint userId, string emailTo, string message, string topic);
    }
}
