using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebRunApplication.Domain.Entities;
using WebRunApplication.Infrastructure.Interfaces;
using WebRunApplication.Services.Extensions;
using WebRunApplication.Services.Interfaces;
using WebRunApplication.Services.Models;

namespace WebRunApplication.Services.Implementations
{
    // todo: все асинхронные методы должны заканчиваться на Async и иметь в параметрах CancellationToken
    public class AdminService(
        IHelpMessageRepository helpRepository,
        IUserRepository userRepository,
        ILogger<AdminService> logger,
        IBaseRepository<MailingTopic> mailingTopicRepository,
        IBaseRepository<MailingMessage> mailingRepository,
        IBaseRepository<MailingTopicSubscriber> mailingTopicSubscriberRepository)
        : IAdminService
    {
        public async Task<IBaseResponse<bool>> CreateAnswerAsync
        (
            uint id,
            string answer,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var model = (await helpRepository.GetHelpMessagesAsync(cancellationToken))
                    .FirstOrDefault(help => help.Id == id);
                
                if (model is null)
                {
                    return new BaseResponse<bool>
                    {
                        Description = "Данной записи не существует",
                        StatusCode = Domain.Enums.StatusCode.NotFound
                    };
                }
                
                model.Answer = answer;

                var user = (await userRepository.GetUsersAsync(cancellationToken))
                    .FirstOrDefault(user => user.Id == model.UserId);

                if (user is null)
                {
                    return new BaseResponse<bool>
                    {
                        Description = "Данный пользователь не найден",
                        StatusCode = Domain.Enums.StatusCode.NotFound
                    };
                }

                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    return new BaseResponse<bool>
                    {
                        Description = "У пользователя не задан Email",
                        StatusCode = Domain.Enums.StatusCode.NotFound
                    };
                }

                var userEmail = user.Email;

                // todo: в конфиг
                var sender = new MailSender("runapp90@mail.ru", userEmail, "RunApp");

                await sender.Send(
                    "Ответ на вопрос",
                    $"Администратор дал ответ на ваш вопрос.\nВопрос: {model.Question}\nОтвет: {model.Answer}"
                );

                await helpRepository.UpdateHelpMessageAsync(
                    model.Id,
                    model.ToHelpMessageWithoutId(),
                    cancellationToken);

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

        public async Task<IBaseResponse<List<HelpMessageViewModel>>> GetQuestionsAsync
        (
            CancellationToken cancellationToken
        )
        {
            try
            {
                // todo: userFIO может выбить ошибку, нужно обработать случай когда user == null

                var helpMessageViewModels = new List<HelpMessageViewModel>();

                var helpMessages = (await helpRepository.GetHelpMessagesAsync(cancellationToken))
                    .ToList();
                
                foreach (var helpMessage in helpMessages)
                {
                    helpMessageViewModels.Add(new HelpMessageViewModel
                    {
                        UserId = helpMessage.UserId,
                        Answer = helpMessage.Answer,
                        Question = helpMessage.Question,
                        Date = helpMessage.Date,
                        Id = helpMessage.Id,
                        UserFIO = (await userRepository
                                .GetUsersAsync(cancellationToken))
                            .FirstOrDefault(user => user.Id == helpMessage.UserId)?
                            .Fullname ?? throw new ArgumentNullException()
                    });
                }

                return new BaseResponse<List<HelpMessageViewModel>>
                {
                    Data = helpMessageViewModels.OrderBy(hm => hm.Answer == null).ToList(),
                    StatusCode = Domain.Enums.StatusCode.OK
                };
            }
            catch(Exception exception)
            {
                logger.LogError(exception, $"[{nameof(AdminService)}]: {exception.Message}");
                return new BaseResponse<List<HelpMessageViewModel>>
                {
                    Description = exception.Message,
                    StatusCode = Domain.Enums.StatusCode.InternalServerError
                };
            }
        }

        // todo: разобраться с async
        public async Task<IBaseResponse<Dictionary<string, (int, int, int)>>> GetTopicsInformationAsync
        (
            CancellationToken cancellationToken
        )
        {
            try
            {
                
                var dict = mailingTopicRepository
                    .GetAll()
                    .Select(t => new
                    {
                        Title = t.Title,
                        SubscribeCount = mailingRepository
                            .GetAll()
                            .Count(x => x.MailingTopicId == t.Id),
                        UserCount = mailingTopicSubscriberRepository
                            .GetAll()
                            .Count(x => x.MailingTopicId == t.Id),
                        Subscribers = mailingTopicSubscriberRepository
                            .GetAll()
                            .Where(x => x.MailingTopicId == t.Id)
                            .GroupBy(x => x.UserId)
                            .Count()
                    })
                    .ToDictionary(
                        x => x.Title, 
                        x => (x.SubscribeCount, x.UserCount * x.SubscribeCount, x.Subscribers)
                    );

                return new BaseResponse<Dictionary<string, (int, int, int)>>
                {
                    Data = dict,
                    StatusCode = Domain.Enums.StatusCode.OK
                };
            }
            catch(Exception exception)
            {
                logger.LogError(exception, $"[{nameof(AdminService)}]: {exception.Message}");
                return new BaseResponse<Dictionary<string, (int, int, int)>>
                {
                    Description = exception.Message,
                    StatusCode = Domain.Enums.StatusCode.InternalServerError
                };
            }
        }
    }
}
