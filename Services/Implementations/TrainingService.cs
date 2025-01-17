using Microsoft.EntityFrameworkCore;
using WebRunApplication.Domain.Enums.DAL.Interfaces;
using WebRunApplication.Domain.Entities;
using WebRunApplication.Domain.Enums.Interfaces;
using WebRunApplication.Domain.Enums.Response;
using WebRunApplication.Domain.Enums.Services.Interfaces;

namespace WebRunApplication.Domain.Enums.Services.Implementations
{
    public class TrainingService : ITrainingService
    {
        private readonly ILogger<TrainingService> _logger;
        private readonly IBaseRepository<Training> _trainingRepository;

        public TrainingService(ILogger<TrainingService> logger, IBaseRepository<Training> trainingRepository)
        {
            _logger = logger;
            _trainingRepository = trainingRepository;
        }

        public async Task<IBaseResponse<Training>> Create(Training model)
        {
            try
            {
                await _trainingRepository.Create(model);

                return new BaseResponse<Training>
                {
                    Data = model,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[{nameof(TrainingService)}.{nameof(Create)}] error: {exception.Message}");
                return new BaseResponse<Training>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {exception.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> Delete(long id)
        {
            try
            {
                var training = await _trainingRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (training is null)
                {
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.NotFound,
                        Data = false
                    };
                }

                await _trainingRepository.Delete(training);
                _logger.LogInformation($"[{nameof(TrainingService)}.{nameof(Delete)}] рассылка удалена");

                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[{nameof(TrainingService)}.{nameof(Delete)}] error: {exception.Message}");
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {exception.Message}"
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Training>>> GetAll()
        {
            try
            {
                var trainings = await _trainingRepository.GetAll().ToListAsync();
                _logger.LogInformation($"[{nameof(TrainingService)}.{nameof(GetAll)}] получено рассылок {trainings.Count}");

                return new BaseResponse<IEnumerable<Training>>
                {
                    Data = trainings,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[{nameof(TrainingService)}.{nameof(GetAll)}] error: {exception.Message}");
                return new BaseResponse<IEnumerable<Training>>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {exception.Message}"
                };
            }
        }
    }
}
