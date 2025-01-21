using Microsoft.Extensions.Logging;
using WebRunApplication.Domain.Entities;
using WebRunApplication.Domain.Enums;
using WebRunApplication.Infrastructure.Interfaces;
using WebRunApplication.Services.Extensions;
using WebRunApplication.Services.Interfaces;

namespace WebRunApplication.Services.Implementations
{
    public class HelpMessageMessageService
    (
        ILogger<HelpMessageMessageService> logger,
        IHelpMessageRepository helpRepository
    ) : IHelpMessageService
    {
        public async Task<IBaseResponse<HelpMessage>> CreateAsync
        (
            HelpMessage model,
            CancellationToken cancellationToken
        )
        {
            try
            {
                await helpRepository.CreateHelpMessageAsync(
                    model.ToHelpMessageWithoutId(),
                    cancellationToken);

                return new BaseResponse<HelpMessage>
                {
                    Data = model,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception exception)
            {
                logger.LogError(exception, $"[{nameof(HelpMessageMessageService)}.{nameof(CreateAsync)}] error: {exception.Message}");
                return new BaseResponse<HelpMessage>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {exception.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var help = await helpRepository.GetHelpMessageByIdAsync(id, cancellationToken);
                if (help is null)
                {
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.NotFound,
                        Data = false
                    };
                }

                await helpRepository.DeleteHelpMessage(help.Id, cancellationToken);
                logger.LogInformation($"[{nameof(HelpMessageMessageService)}.{nameof(DeleteAsync)}] вопрос удален");

                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }
            catch (Exception exception)
            {
                logger.LogError(exception, $"[{nameof(HelpMessageMessageService)}.{nameof(DeleteAsync)}] error: {exception.Message}");
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {exception.Message}"
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<HelpMessage>>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var helps = (await helpRepository.GetHelpMessagesAsync(cancellationToken))
                    .ToList();
                
                logger.LogInformation($"[{nameof(HelpMessageMessageService)}.{nameof(GetAllAsync)}] получено вопросов {helps.Count}");

                return new BaseResponse<IEnumerable<HelpMessage>>
                {
                    Data = helps,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception exception)
            {
                logger.LogError(exception, $"[{nameof(HelpMessageMessageService)}.{nameof(GetAllAsync)}] error: {exception.Message}");
                return new BaseResponse<IEnumerable<HelpMessage>>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {exception.Message}"
                };
            }
        }
    }
}
