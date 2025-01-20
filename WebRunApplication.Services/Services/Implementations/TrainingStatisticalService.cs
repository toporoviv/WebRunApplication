using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebRunApplication.Domain.Entities;
using WebRunApplication.Infrastructure.Interfaces;
using WebRunApplication.Services.Models;
using WebRunApplication.Services.Services.Interfaces;

namespace WebRunApplication.Services.Services.Implementations
{
    // todo: логин в каждом методе нужно провалидировать
    public class TrainingStatisticalService
    (
        IBaseRepository<Indicator> indicatorRepository,
        IBaseRepository<Training> trainingRepository,
        IUserRepository userRepository,
        IBaseRepository<TrainingTemplate> trainingTemplateRepository,
        IBaseRepository<MailingMessage> mailingRepository,
        ILogger<TrainingStatisticalService> logger
    ) : ITrainingStatisticalService
    {
        // todo: _trainingTemplateRepository не используется((
        private readonly IBaseRepository<TrainingTemplate> _trainingTemplateRepository = trainingTemplateRepository;

        public async Task<IBaseResponse<List<TrainingStatisticalMailingCount>>> GetTotalMailingCountAsync
        (
            string login,
            CancellationToken cancellationToken
        )
        {
            try
            {
                // todo: пересмотреть логику + юзер не используется + user может быть null
                var user = (await userRepository.GetUsersAsync(cancellationToken))
                    .FirstOrDefault(user => user.Login == login);

                var data = mailingRepository.GetAll()
                .GroupBy(x => x.Date)
                .Select(x => new TrainingStatisticalMailingCount
                {
                    Date = x.Key,
                    Count = (uint)x.Count()
                })
                .OrderBy(x => x.Date)
                .ToList();

                return new BaseResponse<List<TrainingStatisticalMailingCount>>
                {
                    Data = data,
                    StatusCode = Domain.Enums.StatusCode.OK
                };
            }
            catch(Exception exception)
            {
                logger.LogError(exception, $"[{nameof(TrainingStatisticalService)}.{nameof(GetTotalMailingCountAsync)}]: {exception.Message}");
                return new BaseResponse<List<TrainingStatisticalMailingCount>>
                {
                    Description = exception.Message,
                    StatusCode = Domain.Enums.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<TrainingStatisticalCountViewModel>>> GetTotalTrainingCountAsync
        (
            string login,
            CancellationToken cancellationToken
        )
        {
            try
            {
                // todo: user может быть null
                var user = (await userRepository.GetUsersAsync(cancellationToken))
                    .FirstOrDefault(user => user.Login == login);

                var data = indicatorRepository.GetAll()
                    .Where(x => x.UserId == user.Id)
                    .Join(
                        trainingRepository.GetAll(),
                        indicator => indicator.Date,
                        training => training.Date,
                        (indicator, training) => training)
                    .GroupBy(x => x.Date)
                    .Select(x => new TrainingStatisticalCountViewModel
                    {
                        Date = x.Key,
                        Count = (uint)x.Count()
                    })
                    .OrderBy(x => x.Date)
                    .ToList();

                return new BaseResponse<List<TrainingStatisticalCountViewModel>>
                {
                    Data = data,
                    StatusCode = Domain.Enums.StatusCode.OK
                };
            }
            catch(Exception exception)
            {
                logger.LogError(
                    exception, 
                    $"[{nameof(TrainingStatisticalService)}.{nameof(GetTotalTrainingCountAsync)}]: {exception.Message}"
                );
                
                return new BaseResponse<List<TrainingStatisticalCountViewModel>>
                {
                    Description = exception.Message,
                    StatusCode = Domain.Enums.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<TrainingStatisticalTotalDurationView>>> GetTotalTrainingDayDurationAsync
        (
            string login,
            CancellationToken cancellationToken
        )
        {
            try
            {
                // todo: user может быть null
                var user = (await userRepository.GetUsersAsync(cancellationToken))
                    .FirstOrDefault(user => user.Login == login);

                var data = indicatorRepository.GetAll().ToList()
                    .Where(x => x.UserId == user.Id)
                    .Join(
                        trainingRepository.GetAll().ToList(),
                        indicator => indicator.Date,
                        training => training.Date,
                        (_, training) => training)
                    .GroupBy(x => x.Date)
                    .Select(x => new TrainingStatisticalTotalDurationView
                    {
                        Date = x.Key,
                        TotalDuration = new TimeSpan(0, x.Sum(y => (int)y.Duration.TotalMinutes), 0)
                    })
                    .OrderBy(x => x.Date)
                    .ToList();

                return new BaseResponse<List<TrainingStatisticalTotalDurationView>>
                {
                    Data = data,
                    StatusCode = Domain.Enums.StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                logger.LogError(exception, $"[{nameof(TrainingStatisticalService)}.{nameof(GetTotalTrainingDayDurationAsync)}]: {exception.Message}");
                return new BaseResponse<List<TrainingStatisticalTotalDurationView>>
                {
                    Description = exception.Message,
                    StatusCode = Domain.Enums.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<TrainingStatisticalTotalDurationView>>> GetTotalTrainingDurationAsync
        (
            string login,
            CancellationToken cancellationToken
        )
        {
            try
            {
                // todo: user может быть null
                var user = (await userRepository.GetUsersAsync(cancellationToken))
                    .FirstOrDefault(user => user.Login == login);

                var data = indicatorRepository.GetAll().ToList()
                    .Where(x => x.UserId == user.Id)
                    .Join(
                        trainingRepository.GetAll().ToList(),
                        indicator => indicator.Date,
                        training => training.Date, 
                        (_, training) => training)
                    .GroupBy(x => x.Date)
                    .Select(x => new TrainingStatisticalTotalDurationView
                    {
                        Date = x.Key.Date,
                        TotalDuration = new TimeSpan(0, 0, x.Sum(y => (int)y.Duration.TotalSeconds))
                    })
                    .OrderBy(x => x.Date)
                    .ToList();

                return new BaseResponse<List<TrainingStatisticalTotalDurationView>>
                {
                    Data = data,
                    StatusCode = Domain.Enums.StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                logger.LogError(
                    exception, 
                    $"[{nameof(TrainingStatisticalService)}.{nameof(GetTotalTrainingDurationAsync)}]: {exception.Message}"
                );
                return new BaseResponse<List<TrainingStatisticalTotalDurationView>>
                {
                    Description = exception.Message,
                    StatusCode = Domain.Enums.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<double>>> GetTotalMailingCountGroupByYearAndMonthAsync
        (
            string login,
            CancellationToken cancellationToken
        )
        {
            try
            {
                // todo: user может быть null + юзер не используется
                var user = (await userRepository.GetUsersAsync(cancellationToken))
                    .FirstOrDefault(user => user.Login == login);

                var data = mailingRepository
                    .GetAll()
                    .GroupBy(x => new { x.Date.Year, x.Date.Month })
                    .Select(x => (double)x.Count())
                    .ToList();

                return new BaseResponse<List<double>>
                {
                    Data = data,
                    StatusCode = Domain.Enums.StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                logger.LogError(
                    exception, 
                    $"[{nameof(TrainingStatisticalService)}.{nameof(GetTotalMailingCountGroupByYearAndMonthAsync)}]: {exception.Message}"
                );
                return new BaseResponse<List<double>>
                {
                    Description = exception.Message,
                    StatusCode = Domain.Enums.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<double>>> GetTotalTrainingCountGroupByYearAndMonthAsync
        (
            string login,
            CancellationToken cancellationToken
        )
        {
            try
            {
                // todo: user может быть null
                var user = (await userRepository.GetUsersAsync(cancellationToken))
                    .FirstOrDefault(user => user.Login == login);

                var data = indicatorRepository
                    .GetAll()
                    .Where(x => x.UserId == user.Id)
                    .Join(
                        trainingRepository.GetAll(),
                        indicator => indicator.Date,
                        training => training.Date,
                        (indicator, training) => training)
                    .GroupBy(x => new { x.Date.Year, x.Date.Month })
                    .Select(x => (double)x.Count())
                    .ToList();

                return new BaseResponse<List<double>>
                {
                    Data = data,
                    StatusCode = Domain.Enums.StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                logger.LogError(exception, $"[{nameof(TrainingStatisticalService)}.{nameof(GetTotalTrainingCountGroupByYearAndMonthAsync)}]: {exception.Message}");
                return new BaseResponse<List<double>>
                {
                    Description = exception.Message,
                    StatusCode = Domain.Enums.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<double>>> GetTotalTrainingDurationGroupByYearAndMonthAsync
        (
            string login,
            CancellationToken cancellationToken
        )
        {
            try
            {
                // todo: user может быть null
                var user = (await userRepository.GetUsersAsync(cancellationToken))
                    .FirstOrDefault(user => user.Login == login);

                var data = indicatorRepository.GetAll().ToList()
                    .Where(x => x.UserId == user.Id)
                    .Join(trainingRepository.GetAll().ToList(), 
                        indicator => indicator.Date,
                        training => training.Date, 
                        (_, training) => training)
                    .GroupBy(x => new { x.Date.Year, x.Date.Month })
                    .Select(x => (double)x.Sum(y => (int)y.Duration.TotalMinutes))
                    .ToList();

                return new BaseResponse<List<double>>
                {
                    Data = data,
                    StatusCode = Domain.Enums.StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                logger.LogError(exception, $"[{nameof(TrainingStatisticalService)}.{nameof(GetTotalTrainingDurationGroupByYearAndMonthAsync)}]: {exception.Message}");
                return new BaseResponse<List<double>>
                {
                    Description = exception.Message,
                    StatusCode = Domain.Enums.StatusCode.InternalServerError
                };
            }
        }
    }
}
