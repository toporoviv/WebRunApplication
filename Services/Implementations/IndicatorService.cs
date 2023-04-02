using Microsoft.EntityFrameworkCore;
using WebRunApplication.DAL.Interfaces;
using WebRunApplication.DAL.Repositories;
using WebRunApplication.DataEntity;
using WebRunApplication.Enums;
using WebRunApplication.Interfaces;
using WebRunApplication.Response;
using WebRunApplication.Services.Interfaces;

namespace WebRunApplication.Services.Implementations
{
    public class IndicatorService : IIndicatorService
    {
        private readonly IBaseRepository<Indicator> _indicatorRepository;
        private readonly ILogger<IndicatorService> _logger;

        public IndicatorService(
            IBaseRepository<Indicator> indicatorRepository,
            ILogger<IndicatorService> logger)
        {
            _indicatorRepository = indicatorRepository;
            _logger = logger;
        }

        public async Task<IBaseResponse<Indicator>> Create(Indicator model)
        {
            try
            {
                await _indicatorRepository.Create(model);

                return new BaseResponse<Indicator>()
                {
                    Data = model,
                    Description = "Показатель добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(IndicatorService)}.{nameof(Create)}] error: {ex.Message}");
                return new BaseResponse<Indicator>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Indicator>>> GetAll()
        {
            try
            {
                var indicators = await _indicatorRepository.GetAll().ToListAsync();

                _logger.LogInformation($"[{nameof(IndicatorService)}.{nameof(GetAll)}] получено элементов {indicators.Count}");
                return new BaseResponse<IEnumerable<Indicator>>()
                {
                    Data = indicators,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(IndicatorService)}.{nameof(GetAll)}] error: {ex.Message}");
                return new BaseResponse<IEnumerable<Indicator>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> Delete(long id)
        {
            try
            {
                var indicator = await _indicatorRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (indicator is null)
                {
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.NotFound,
                        Data = false
                    };
                }

                await _indicatorRepository.Delete(indicator);
                _logger.LogInformation($"{nameof(IndicatorService)}.{nameof(Delete)} показатель удален");

                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{nameof(IndicatorService)}.{nameof(Delete)}] error: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
