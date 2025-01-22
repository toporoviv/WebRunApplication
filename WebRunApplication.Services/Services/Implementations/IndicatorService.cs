using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebRunApplication.Domain.Entities;
using WebRunApplication.Domain.Enums;
using WebRunApplication.Infrastructure.Interfaces;
using WebRunApplication.Services.Extensions;
using WebRunApplication.Services.Interfaces;

namespace WebRunApplication.Services.Implementations;

public class IndicatorService(
    IIndicatorRepository indicatorRepository,
    ILogger<IndicatorService> logger)
    : IIndicatorService
{
    public async Task<IBaseResponse<Indicator>> CreateAsync
    (
        Infrastructure.Models.Indicator model,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var result = await indicatorRepository.CreateIndicatorAsync(model, cancellationToken);

            return new BaseResponse<Indicator>()
            {
                Data = result,
                Description = "Показатель добавлен",
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"[{nameof(IndicatorService)}.{nameof(CreateAsync)}] error: {ex.Message}");
            return new BaseResponse<Indicator>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = $"Внутренняя ошибка: {ex.Message}"
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<Indicator>>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            var indicators = (await indicatorRepository.GetIndicatorsAsync(cancellationToken))
                .ToList();

            logger.LogInformation($"[{nameof(IndicatorService)}.{nameof(GetAllAsync)}] получено элементов {indicators.Count}");
            return new BaseResponse<IEnumerable<Indicator>>()
            {
                Data = indicators,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"[{nameof(IndicatorService)}.{nameof(GetAllAsync)}] error: {ex.Message}");
            return new BaseResponse<IEnumerable<Indicator>>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = $"Внутренняя ошибка: {ex.Message}"
            };
        }
    }

    public async Task<IBaseResponse<bool>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var indicator = await indicatorRepository.GetIndicatorByIdAsync(id, cancellationToken);
            if (indicator is null)
            {
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.NotFound,
                    Data = false
                };
            }

            await indicatorRepository.DeleteIndicatorAsync(indicator.Id, cancellationToken);
            logger.LogInformation($"{nameof(IndicatorService)}.{nameof(DeleteAsync)} показатель удален");

            return new BaseResponse<bool>
            {
                StatusCode = StatusCode.OK,
                Data = true
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"[{nameof(IndicatorService)}.{nameof(DeleteAsync)}] error: {ex.Message}");
            return new BaseResponse<bool>
            {
                StatusCode = StatusCode.InternalServerError,
                Description = $"Внутренняя ошибка: {ex.Message}"
            };
        }
    }
}