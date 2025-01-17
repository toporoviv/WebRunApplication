using WebRunApplication.Domain.Entities;
using WebRunApplication.Domain.Enums.Interfaces;
using WebRunApplication.Domain.Enums.Response;

namespace WebRunApplication.Domain.Enums.Services.Interfaces
{
    public interface IMailingTopicService : IBaseService<MailingTopic>
    {
        Task<IBaseResponse<bool>> Update(MailingTopic model);
    }
}
