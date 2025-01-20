using WebRunApplication.Domain.Entities;

namespace WebRunApplication.Services.Services.Interfaces
{
    public interface IMailingTopicService : IBaseService<MailingTopic>
    {
        Task<IBaseResponse<bool>> Update(MailingTopic model);
    }
}
