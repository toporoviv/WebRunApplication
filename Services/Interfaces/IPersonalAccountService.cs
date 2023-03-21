using WebRunApplication.DataEntity;
using WebRunApplication.Interfaces;
using WebRunApplication.Models;

namespace WebRunApplication.Services.Interfaces
{
    public interface IPersonalAccountService
    {
        Task<IBaseResponse<List<MailingTopic>>> GetMailingTopics();

        Task<IBaseResponse<bool>> CreateSubscribe(string login, int[] titles);

        Task<IBaseResponse<List<TrainingInformation>>> GetTrainings(string login);
    }
}
