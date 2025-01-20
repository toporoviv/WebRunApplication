using WebRunApplication.Domain.Entities;
using WebRunApplication.Services.Models;

namespace WebRunApplication.Services.Services.Interfaces
{
    public interface IPersonalAccountService
    {
        Task<IBaseResponse<List<MailingTopic>>> GetMailingTopics();

        Task<IBaseResponse<bool>> CreateSubscribe(string login, int[] titles);

        Task<IBaseResponse<List<TrainingInformation>>> GetTrainings(string login);
    }
}
