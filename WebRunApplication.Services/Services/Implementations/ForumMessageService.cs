using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebRunApplication.Domain.Entities.Forum;
using WebRunApplication.Domain.Enums;
using WebRunApplication.Infrastructure.Interfaces;
using WebRunApplication.Services.Extensions;
using WebRunApplication.Services.Interfaces;

namespace WebRunApplication.Services.Implementations
{
    internal class ForumMessageService(
        ILogger<ForumMessageService> logger,
        IForumMessageRepository forumMessageRepository)
        : IForumMessageService
    {
        public async Task<IBaseResponse<ForumMessage>> CreateAsync
        (
            ForumMessage model,
            CancellationToken cancellationToken
        )
        {
            try
            {
                await forumMessageRepository.CreateForumMessageAsync(
                    model.ToForumMessageWithoutId(),
                    cancellationToken);

                return new BaseResponse<ForumMessage>
                {
                    Data = model,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception exception)
            {
                logger.LogError(exception, $"[{nameof(ForumMessageService)}.{nameof(CreateAsync)}] error: {exception.Message}");
                return new BaseResponse<ForumMessage>()
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
                var message = await forumMessageRepository.GetForumMessageByIdAsync(id, cancellationToken);
                
                if (message is null)
                {
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.NotFound,
                        Data = false
                    };
                }

                await forumMessageRepository.DeleteForumMessageAsync(message.Id, cancellationToken);
                logger.LogInformation($"[{nameof(ForumMessageService)}.{nameof(DeleteAsync)}] сообщение удалено (id = {message.Id})");

                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }
            catch (Exception exception)
            {
                logger.LogError(exception, $"[{nameof(ForumMessageService)}.{nameof(DeleteAsync)}] error: {exception.Message}");
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {exception.Message}"
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<ForumMessage>>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var messages = (await forumMessageRepository.GetForumMessages(cancellationToken))
                    .ToList();
                
                logger.LogInformation($"[{nameof(ForumMessageService)}.{nameof(GetAllAsync)}] получено сообщений {messages.Count}");

                return new BaseResponse<IEnumerable<ForumMessage>>
                {
                    Data = messages,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception exception)
            {
                logger.LogError(exception, $"[{nameof(ForumMessageService)}.{nameof(GetAllAsync)}] error: {exception.Message}");
                return new BaseResponse<IEnumerable<ForumMessage>>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {exception.Message}"
                };
            }
        }
    }
}
