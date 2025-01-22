using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebRunApplication.Domain.Entities.Forum;
using WebRunApplication.Domain.Enums;
using WebRunApplication.Infrastructure.Interfaces;
using WebRunApplication.Services.Interfaces;

namespace WebRunApplication.Services.Implementations;

internal class ForumReactionService
(
    ILogger<ForumReactionService> logger,
    IForumReactionRepository forumReactionRepository
) : IForumReactionService
{
    public async Task<IBaseResponse<ForumReaction>> CreateAsync
    (
        Infrastructure.Models.ForumReaction model,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var result = await forumReactionRepository.CreateForumReactionAsync(model, cancellationToken);

            return new BaseResponse<ForumReaction>
            {
                Data = result,
                StatusCode = StatusCode.OK,
            };
        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[{nameof(ForumReactionService)}.{nameof(CreateAsync)}] error: {exception.Message}");
            return new BaseResponse<ForumReaction>()
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
            var reaction = await forumReactionRepository.GetForumReactionByIdAsync(id, cancellationToken);
                
            if (reaction is null)
            {
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.NotFound,
                    Data = false
                };
            }

            await forumReactionRepository.DeleteForumReactionAsync(reaction.Id, cancellationToken);
            logger.LogInformation($"[{nameof(ForumReactionService)}.{nameof(DeleteAsync)}] реакция удалена");

            return new BaseResponse<bool>
            {
                StatusCode = StatusCode.OK,
                Data = true
            };
        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[{nameof(ForumReactionService)}.{nameof(DeleteAsync)}] error: {exception.Message}");
            return new BaseResponse<bool>()
            {
                StatusCode = StatusCode.InternalServerError,
                Description = $"Внутренняя ошибка: {exception.Message}"
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<ForumReaction>>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            var reactions = (await forumReactionRepository.GetForumReactionsAsync(cancellationToken))
                .ToList();
                
            logger.LogInformation($"[{nameof(ForumReactionService)}.{nameof(GetAllAsync)}] получено реакций {reactions.Count}");

            return new BaseResponse<IEnumerable<ForumReaction>>
            {
                Data = reactions,
                StatusCode = StatusCode.OK,
            };
        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[{nameof(ForumReactionService)}.{nameof(GetAllAsync)}] error: {exception.Message}");
            return new BaseResponse<IEnumerable<ForumReaction>>
            {
                StatusCode = StatusCode.InternalServerError,
                Description = $"Внутренняя ошибка: {exception.Message}"
            };
        }
    }
}