using Microsoft.EntityFrameworkCore;
using WebRunApplication.DAL.Interfaces;
using WebRunApplication.DAL.Repositories;
using WebRunApplication.DataEntity;
using WebRunApplication.Interfaces;
using WebRunApplication.Models;
using WebRunApplication.Response;
using WebRunApplication.Services.Interfaces;

namespace WebRunApplication.Services.Implementations
{
    public class TrainingStatisticalService : ITrainingStatisticalService
    {
        private readonly IBaseRepository<Indicator> _indicatorRepository;
        private readonly IBaseRepository<Training> _trainingRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<TrainingTemplate> _trainingTemplateRepository;
        private readonly IBaseRepository<Mailing> _mailingRepository;
        private readonly ILogger<TrainingStatisticalService> _logger;

        public TrainingStatisticalService(
            IBaseRepository<Indicator> indicatorRepository,
            IBaseRepository<Training> trainingRepository,
            IBaseRepository<User> userRepository,
            IBaseRepository<TrainingTemplate> trainingTemplateRepository,
            IBaseRepository<Mailing> mailingRepository,
            ILogger<TrainingStatisticalService> logger
            )
        {
            _indicatorRepository = indicatorRepository;
            _trainingRepository = trainingRepository;
            _userRepository = userRepository;
            _trainingTemplateRepository = trainingTemplateRepository;
            _mailingRepository = mailingRepository;
            _logger = logger;
        }

        public async Task<IBaseResponse<List<TrainingStatisticalMailingCount>>> GetTotalMailingCount(string login)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == login);

                var data = _mailingRepository.GetAll()
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
                    StatusCode = Enums.StatusCode.OK
                };
            }
            catch(Exception exception)
            {
                _logger.LogError(exception, $"[{nameof(TrainingStatisticalService)}.{nameof(GetTotalMailingCount)}]: {exception.Message}");
                return new BaseResponse<List<TrainingStatisticalMailingCount>>
                {
                    Description = exception.Message,
                    StatusCode = Enums.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<TrainingStatisticalCountViewModel>>> GetTotalTrainingCount(string login)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == login);

                var data = _indicatorRepository.GetAll()
                    .Where(x => x.UserId == user.Id)
                    .Join(_trainingRepository.GetAll(), indicator => indicator.Date, training => training.Date, (indicator, training) => training)
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
                    StatusCode = Enums.StatusCode.OK
                };
            }
            catch(Exception exception)
            {
                _logger.LogError(exception, $"[{nameof(TrainingStatisticalService)}.{nameof(GetTotalTrainingCount)}]: {exception.Message}");
                return new BaseResponse<List<TrainingStatisticalCountViewModel>>
                {
                    Description = exception.Message,
                    StatusCode = Enums.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<TrainingStatisticalTotalDurationView>>> GetTotalTrainingDayDuration(string login)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == login);

                var data = _indicatorRepository.GetAll().ToList()
                    .Where(x => x.UserId == user.Id)
                    .Join(_trainingRepository.GetAll().ToList(), indicator => indicator.Date, training => training.Date, (indicator, training) => training)
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
                    StatusCode = Enums.StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[{nameof(TrainingStatisticalService)}.{nameof(GetTotalTrainingDayDuration)}]: {exception.Message}");
                return new BaseResponse<List<TrainingStatisticalTotalDurationView>>
                {
                    Description = exception.Message,
                    StatusCode = Enums.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<TrainingStatisticalTotalDurationView>>> GetTotalTrainingDuration(string login)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == login);

                var data = _indicatorRepository.GetAll().ToList()
                    .Where(x => x.UserId == user.Id)
                    .Join(_trainingRepository.GetAll().ToList(), indicator => indicator.Date, training => training.Date, (indicator, training) => training)
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
                    StatusCode = Enums.StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[{nameof(TrainingStatisticalService)}.{nameof(GetTotalTrainingDuration)}]: {exception.Message}");
                return new BaseResponse<List<TrainingStatisticalTotalDurationView>>
                {
                    Description = exception.Message,
                    StatusCode = Enums.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<double>>> GetTotalMailingCountGroupByYearAndMonth(string login)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == login);

                var data = _mailingRepository.GetAll()
                .GroupBy(x => new { x.Date.Year, x.Date.Month })
                .Select(x => (double)x.Count())
                .ToList();

                return new BaseResponse<List<double>>
                {
                    Data = data,
                    StatusCode = Enums.StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[{nameof(TrainingStatisticalService)}.{nameof(GetTotalMailingCountGroupByYearAndMonth)}]: {exception.Message}");
                return new BaseResponse<List<double>>
                {
                    Description = exception.Message,
                    StatusCode = Enums.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<double>>> GetTotalTrainingCountGroupByYearAndMonth(string login)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == login);

                var data = _indicatorRepository.GetAll()
                    .Where(x => x.UserId == user.Id)
                    .Join(_trainingRepository.GetAll(), indicator => indicator.Date, training => training.Date, (indicator, training) => training)
                    .GroupBy(x => new { x.Date.Year, x.Date.Month })
                    .Select(x => (double)x.Count())
                    .ToList();

                return new BaseResponse<List<double>>
                {
                    Data = data,
                    StatusCode = Enums.StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[{nameof(TrainingStatisticalService)}.{nameof(GetTotalTrainingCountGroupByYearAndMonth)}]: {exception.Message}");
                return new BaseResponse<List<double>>
                {
                    Description = exception.Message,
                    StatusCode = Enums.StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<double>>> GetTotalTrainingDurationGroupByYearAndMonth(string login)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == login);

                var data = _indicatorRepository.GetAll().ToList()
                    .Where(x => x.UserId == user.Id)
                    .Join(_trainingRepository.GetAll().ToList(), indicator => indicator.Date, training => training.Date, (indicator, training) => training)
                    .GroupBy(x => new { x.Date.Year, x.Date.Month })
                    .Select(x => (double)x.Sum(y => (int)y.Duration.TotalMinutes))
                    .ToList();

                return new BaseResponse<List<double>>
                {
                    Data = data,
                    StatusCode = Enums.StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[{nameof(TrainingStatisticalService)}.{nameof(GetTotalTrainingDurationGroupByYearAndMonth)}]: {exception.Message}");
                return new BaseResponse<List<double>>
                {
                    Description = exception.Message,
                    StatusCode = Enums.StatusCode.InternalServerError
                };
            }
        }
    }
}
