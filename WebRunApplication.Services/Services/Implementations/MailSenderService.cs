using Microsoft.Extensions.Logging;
using WebRunApplication.Domain.Entities;
using WebRunApplication.Infrastructure.Interfaces;
using WebRunApplication.Services.Interfaces;
using WebRunApplication.Services.Models;

namespace WebRunApplication.Services.Implementations
{
    public class MailSenderService(IHelpMessageRepository helpMessageRepository, ILogger<MailSenderService> logger)
        : IMailSenderService
    {
        public async Task<IBaseResponse<bool>> SendMessageAsync
        (
            uint userId,
            string emailTo,
            string message,
            string topic,
            CancellationToken cancellationToken
        )
        {
            try
            {
                // todo: нужно вынести в конфиг почту
                var mailSender = new MailSender("runapp90@mail.ru", emailTo, "RunApp");

                await mailSender.Send(topic, message);

                await helpMessageRepository.CreateHelpMessageAsync(new Infrastructure.Models.HelpMessage
                {
                    Date = DateTime.Now,
                    UserId = (int)userId,
                    Question = message
                }, cancellationToken);

                return new BaseResponse<bool>
                {
                    Data = true,
                    StatusCode = Domain.Enums.StatusCode.OK
                };
            }
            catch(Exception exception)
            {
                logger.LogError(exception, $"[{nameof(AdminService)}]: {exception.Message}");
                return new BaseResponse<bool>
                {
                    Description = exception.Message,
                    StatusCode = Domain.Enums.StatusCode.InternalServerError
                };
            }
        }
    }
}
