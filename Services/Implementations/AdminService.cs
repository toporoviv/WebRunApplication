using Microsoft.EntityFrameworkCore;
using WebRunApplication.Domain.Entities;
using WebRunApplication.Domain.Enums.Interfaces;
using WebRunApplication.Domain.Enums.Models;
using WebRunApplication.Domain.Enums.Response;
using WebRunApplication.Domain.Enums.Services.Interfaces;
using WebRunApplication.Infrastructure.Interfaces;

namespace WebRunApplication.Domain.Enums.Services.Implementations
{
    // todo: все асинхронные методы должны заканчиваться на Async и иметь в параметрах CancellationToken
    public class AdminService : IAdminService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<MailingTopic> _mailingTopicRepository;
        private readonly IBaseRepository<Mailing> _mailingRepository;
        private readonly IBaseRepository<MailingTopicSubscriber> _mailingTopicSubscriberRepository;
        private readonly IBaseRepository<Help> _helpRepository;
        private readonly ILogger<AdminService> _logger;

        public AdminService
        (
            IBaseRepository<Help> helpRepository, 
            IBaseRepository<User> userRepository,
            ILogger<AdminService> logger, 
            IBaseRepository<MailingTopic> mailingTopicRepository,
            IBaseRepository<Mailing> mailingRepository, 
            IBaseRepository<MailingTopicSubscriber> mailingTopicSubscriberRepository
        )
        {
            _helpRepository = helpRepository;
            _logger = logger;
            _userRepository = userRepository;
            _mailingTopicRepository = mailingTopicRepository;
            _mailingRepository = mailingRepository;
            _mailingTopicSubscriberRepository = mailingTopicSubscriberRepository;
        }

        public async Task<IBaseResponse<bool>> CreateAnswer(uint id, string answer)
        {
            try
            {
                // todo: model может быть null
                var model = await _helpRepository.GetAll().FirstOrDefaultAsync(help => help.Id == id);
                model.Answer = answer;

                if (model is null)
                {
                    return new BaseResponse<bool>
                    {
                        Description = "Данной записи не существует",
                        StatusCode = Domain.Enums.StatusCode.NotFound
                    };
                }

                var user = await _userRepository.GetAll().FirstOrDefaultAsync(user => user.Id == model.UserId);

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

                await _helpRepository.Update(model);

                return new BaseResponse<bool>
                {
                    Data = true,
                    StatusCode = Domain.Enums.StatusCode.OK
                };
            }
            catch(Exception exception)
            {
                _logger.LogError(exception, $"[{nameof(AdminService)}]: {exception.Message}");
                return new BaseResponse<bool>
                {
                    Description = exception.Message,
                    StatusCode = Domain.Enums.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<HelpViewModel>>> GetQuestions()
        {
            try
            {
                // todo: userFIO может выбить ошибку, нужно обработать случай когда user == null
                var list = _helpRepository
                    .GetAll()
                    .Select(help => new HelpViewModel
                    {
                        UserId = help.UserId,
                        Answer = help.Answer,
                        Question = help.Question,
                        Date = help.Date,
                        Id = help.Id,
                        UserFIO = _userRepository
                            .GetAll()
                            .FirstOrDefault(user => user.Id == help.UserId)
                        .Fullname
                    })
                    .OrderBy(x => x.Answer == null)
                    .ToList();

                return new BaseResponse<List<HelpViewModel>>
                {
                    Data = list,
                    StatusCode = Domain.Enums.StatusCode.OK
                };
            }
            catch(Exception exception)
            {
                _logger.LogError(exception, $"[{nameof(AdminService)}]: {exception.Message}");
                return new BaseResponse<List<HelpViewModel>>
                {
                    Description = exception.Message,
                    StatusCode = Domain.Enums.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Dictionary<string, (int, int, int)>>> GetTopicsInformation()
        {
            try
            {
                
                var dict = _mailingTopicRepository
                    .GetAll()
                    .Select(t => new
                    {
                        Title = t.Title,
                        SubscribeCount = _mailingRepository
                            .GetAll()
                            .Count(x => x.MailingTopicId == t.Id),
                        UserCount = _mailingTopicSubscriberRepository
                            .GetAll()
                            .Count(x => x.MailingTopicId == t.Id),
                        Subscribers = _mailingTopicSubscriberRepository
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
                _logger.LogError(exception, $"[{nameof(AdminService)}]: {exception.Message}");
                return new BaseResponse<Dictionary<string, (int, int, int)>>
                {
                    Description = exception.Message,
                    StatusCode = Domain.Enums.StatusCode.InternalServerError
                };
            }
        }
    }
}
