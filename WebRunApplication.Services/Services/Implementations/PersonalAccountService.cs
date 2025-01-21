using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebRunApplication.Domain.Entities;
using WebRunApplication.Infrastructure.Interfaces;
using WebRunApplication.Services.Interfaces;
using WebRunApplication.Services.Models;

namespace WebRunApplication.Services.Implementations
{
    public class PersonalAccountService(
        IBaseRepository<MailingTopic> mailingTopicRepository,
        ILogger<PersonalAccountService> logger,
        IUserRepository userRepository,
        IBaseRepository<MailingTopicSubscriber> mailingTopicSubscriberRepository,
        IBaseRepository<Training> trainingRepository,
        IBaseRepository<Indicator> indicatorRepository,
        IBaseRepository<TrainingTemplate> trainingTemplateRepository)
    : IPersonalAccountService
    {
        public async Task<IBaseResponse<bool>> CreateSubscribeAsync
        (
            string login,
            int[] titles,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                // todo: user может быть null
                var user = (await userRepository
                    .GetUsersAsync(cancellationToken))
                    .FirstOrDefault(user => user.Login == login);

                var ids = mailingTopicSubscriberRepository
                    .GetAll()
                    .Where(x => x.UserId == user.Id)
                    .Select(x => x.MailingTopicId)
                    .ToList();

                var indexes = titles
                    .Where(x => !ids.Contains(x))
                    .ToList();

                // todo: можно запихать все это в таски и использовать Task.WhenAll
                for (var i = 0; i < indexes.Count; i++)
                {
                    await mailingTopicSubscriberRepository.Create(new MailingTopicSubscriber
                    {
                        MailingTopicId = indexes[i],
                        UserId = user.Id
                    });
                }

                return new BaseResponse<bool>
                {
                    Data = true,
                    StatusCode = Domain.Enums.StatusCode.OK
                };
            }
            catch(Exception exception)
            {
                logger.LogError(exception, $"[{nameof(PersonalAccountService)}]: {exception.Message}");
                return new BaseResponse<bool>
                {
                    Description = exception.Message,
                    StatusCode = Domain.Enums.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<MailingTopic>>> GetMailingTopicsAsync
        (
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                var list = await mailingTopicRepository.GetAll().ToListAsync();

                return new BaseResponse<List<MailingTopic>>
                {
                    Data = list,
                    StatusCode = Domain.Enums.StatusCode.OK
                };
            }
            catch(Exception exception)
            {
                logger.LogError(exception, $"[{nameof(PersonalAccountService)}]: {exception.Message}");
                return new BaseResponse<List<MailingTopic>>
                {
                    Description = exception.Message,
                    StatusCode = Domain.Enums.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<TrainingInformation>>> GetTrainingsAsync
        (
            string login,
            CancellationToken cancellationToken = default
        )
        {
            try
            {
                var user = (await userRepository
                    .GetUsersAsync(cancellationToken))
                    .FirstOrDefault(user => user.Login == login);

                var trainings = await indicatorRepository
                    .GetAll()
                    .Where(indicator => indicator.UserId == user.Id)
                    .Join(
                        trainingRepository.GetAll(),
                        indicator => indicator.Date,
                        training => training.Date,
                        (indicator, training) => new { Indicator = indicator, Training = training }
                    )
                    .Join(
                        trainingTemplateRepository.GetAll(),
                        selector => selector.Training.TrainTemplateId,
                        template => template.Id, 
                        (selector, template) => new { Indicator = selector.Indicator, template.Title }
                    )
                    .Select(result => new TrainingInformation
                    {
                        Id = result.Indicator.Id,
                        AveragePulse = result.Indicator.AveragePulse,
                        MaximumPulse = result.Indicator.MaximumPulse,
                        MinimumPulse = result.Indicator.MinimumPulse,
                        Steps = result.Indicator.Steps,
                        AverageSpeed = result.Indicator.AverageSpeed,
                        Title = result.Title,
                        Calories = result.Indicator.Calories,
                        Date = result.Indicator.Date,
                        Duration = result.Indicator.Duration,
                        DiastolicPressure= result.Indicator.DiastolicPressure,
                        SystolicPressure = result.Indicator.SystolicPressure,
                        UserId = result.Indicator.UserId
                    })
                    .ToListAsync(cancellationToken);

                return new BaseResponse<List<TrainingInformation>>
                {
                    Data = trainings,
                    StatusCode = Domain.Enums.StatusCode.OK
                };
            }
            catch(Exception exception)
            {
                logger.LogError(exception, $"[{nameof(PersonalAccountService)}]: {exception.Message}");
                return new BaseResponse<List<TrainingInformation>>
                {
                    Description = exception.Message,
                    StatusCode = Domain.Enums.StatusCode.InternalServerError
                };
            }
        }
    }
}
