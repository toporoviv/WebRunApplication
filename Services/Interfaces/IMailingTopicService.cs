using WebRunApplication.DataEntity;
using WebRunApplication.Interfaces;
using WebRunApplication.Response;

namespace WebRunApplication.Services.Interfaces
{
    public interface IMailingTopicService : IBaseService<MailingTopic>
    {
        Task<IBaseResponse<bool>> Update(MailingTopic model);
    }
}
