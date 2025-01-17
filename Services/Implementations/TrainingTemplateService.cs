using Microsoft.EntityFrameworkCore;
using WebRunApplication.Domain.Enums.DAL.Interfaces;
using WebRunApplication.Domain.Entities;
using WebRunApplication.Domain.Enums.Interfaces;
using WebRunApplication.Domain.Enums.Response;
using WebRunApplication.Domain.Enums.Services.Interfaces;

namespace WebRunApplication.Domain.Enums.Services.Implementations
{
    public class TrainingTemplateService : ITrainingTemplateService
    {
        private readonly ILogger<TrainingTemplateService> _logger;
        private readonly IBaseRepository<TrainingTemplate> _trainingTemplateRepository;

        public TrainingTemplateService(ILogger<TrainingTemplateService> logger, IBaseRepository<TrainingTemplate> trainingTemplateRepository)
        {
            _logger = logger;
            _trainingTemplateRepository = trainingTemplateRepository;
        }

        public async Task<IBaseResponse<TrainingTemplate>> Create(TrainingTemplate model)
        {
            try
            {
                await _trainingTemplateRepository.Create(model);

                return new BaseResponse<TrainingTemplate>
                {
                    Data = model,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[{nameof(TrainingTemplateService)}.{nameof(Create)}] error: {exception.Message}");
                return new BaseResponse<TrainingTemplate>()
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
                var trainingTemplate = await _trainingTemplateRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (trainingTemplate is null)
                {
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.NotFound,
                        Data = false
                    };
                }

                await _trainingTemplateRepository.Delete(trainingTemplate);
                _logger.LogInformation($"[{nameof(TrainingTemplateService)}.{nameof(Delete)}] сообщение удалено");

                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[{nameof(TrainingTemplateService)}.{nameof(Delete)}] error: {exception.Message}");
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {exception.Message}"
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<TrainingTemplate>>> GetAll()
        {
            try
            {
                var trainings = await _trainingTemplateRepository.GetAll().ToListAsync();
                _logger.LogInformation($"[{nameof(TrainingTemplateService)}.{nameof(GetAll)}] получено сообщений {trainings.Count}");

                return new BaseResponse<IEnumerable<TrainingTemplate>>
                {
                    Data = trainings,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[{nameof(TrainingTemplateService)}.{nameof(GetAll)}] error: {exception.Message}");
                return new BaseResponse<IEnumerable<TrainingTemplate>>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {exception.Message}"
                };
            }
        }
    }
}
