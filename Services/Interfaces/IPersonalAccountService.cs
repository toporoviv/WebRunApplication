using WebRunApplication.Domain.Entities;
using WebRunApplication.Domain.Enums.Interfaces;
using WebRunApplication.Domain.Enums.Models;

namespace WebRunApplication.Domain.Enums.Services.Interfaces
{
    public interface IPersonalAccountService
    {
        Task<IBaseResponse<List<MailingTopic>>> GetMailingTopics();

        Task<IBaseResponse<bool>> CreateSubscribe(string login, int[] titles);

        Task<IBaseResponse<List<TrainingInformation>>> GetTrainings(string login);
    }
}
