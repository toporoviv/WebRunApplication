using WebRunApplication.DAL.Interfaces;
using WebRunApplication.DataEntity;
using WebRunApplication.Interfaces;
using WebRunApplication.Models;
using WebRunApplication.Response;
using WebRunApplication.Services.Interfaces;

namespace WebRunApplication.Services.Implementations
{
    public class MailSenderService : IMailSenderService
    {
        private readonly IBaseRepository<Help> _helpRepository;
        private readonly ILogger<MailSenderService> _logger;

        public MailSenderService(IBaseRepository<Help> helpRepository, ILogger<MailSenderService> logger)
        {
            _helpRepository = helpRepository;
            _logger = logger;
        }

        public async Task<IBaseResponse<bool>> SendMessage(uint userId, string emailTo, string message, string topic)
        {
            try
            {
                var mailSender = new MailSender("runapp90@mail.ru", emailTo, "RunApp");

                await mailSender.Send(topic, message);

                await _helpRepository.Create(new Help { Date = DateTime.Now, UserId = userId, Question = message });

                return new BaseResponse<bool>
                {
                    Data = true,
                    StatusCode = Enums.StatusCode.OK
                };
            }
            catch(Exception exception)
            {
                _logger.LogError(exception, $"[{nameof(AdminService)}]: {exception.Message}");
                return new BaseResponse<bool>
                {
                    Description = exception.Message,
                    StatusCode = Enums.StatusCode.InternalServerError
                };
            }
        }
    }
}
