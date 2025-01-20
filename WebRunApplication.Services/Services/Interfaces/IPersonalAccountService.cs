using WebRunApplication.Domain.Entities;
using WebRunApplication.Services.Models;

namespace WebRunApplication.Services.Services.Interfaces
{
    public interface IPersonalAccountService
    {
        Task<IBaseResponse<List<MailingTopic>>> GetMailingTopicsAsync(CancellationToken cancellationToken = default);

        Task<IBaseResponse<bool>> CreateSubscribeAsync(
            string login,
            int[] titles,
            CancellationToken cancellationToken = default
        );

        Task<IBaseResponse<List<TrainingInformation>>> GetTrainingsAsync(
            string login,
            CancellationToken cancellationToken = default
        );
    }
}
