using Dapper;
using Microsoft.Extensions.Options;
using WebRunApplication.Domain.Entities.Forum;
using WebRunApplication.Infrastructure.Interfaces;
using WebRunApplication.Infrastructure.Options;

namespace WebRunApplication.Infrastructure.Repositories
{
    internal class ForumReactionRepository(IOptions<PostgreOptions> options)
        : DbRepository(options.Value), IForumReactionRepository
    {
        public async Task<ForumReaction> CreateForumReactionAsync
        (
            Models.ForumReaction forumReaction,
            CancellationToken cancellationToken = default
        )
        {
            ArgumentNullException.ThrowIfNull(forumReaction);
            
            await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

            var sqlQuery = @$"insert into forum_reactions(user_id, message_id, reaction)
                            values (@{nameof(forumReaction.UserId)},
                                    @{nameof(forumReaction.MessageId)},
                                    @{nameof(forumReaction.Reaction)})
                            returning id, user_id, message_id, reaction";

            return await connection.QueryFirstAsync<ForumReaction>(sqlQuery);
        }

        public async Task<IEnumerable<ForumReaction>> GetForumReactionsAsync
        (
            CancellationToken cancellationToken = default
        )
        {
            await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

            var sqlQuery = "select * from forum_reactions";

            return await connection.QueryAsync<ForumReaction>(sqlQuery);
        }

        public async Task<ForumReaction?> GetForumReactionByIdAsync
        (
            int id,
            CancellationToken cancellationToken = default
        )
        {
            await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

            var sqlQuery = "select * from forum_reactions where id = @Id";
            var sqlParams = new
            {
                Id = id
            };

            return await connection.QueryFirstOrDefaultAsync<ForumReaction>(sqlQuery, sqlParams);
        }

        public async Task<ForumReaction> UpdateForumReactionAsync
        (
            int id,
            Models.ForumReaction forumReaction,
            CancellationToken cancellationToken = default
        )
        {
            ArgumentNullException.ThrowIfNull(forumReaction);

            await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

            var sqlQuery = @"update forum_reactions
                set user_id = @UserId,
                    message_id = @MessageId,
                    reaction = @Reaction
                where id = @Id
                returning id, user_id, message_id, reaction";

            var sqlParams = new
            {
                Id = id,
                UserId = forumReaction.UserId,
                MessageId = forumReaction.MessageId,
                Reaction = forumReaction.Reaction
            };

            return await connection.QueryFirstAsync<ForumReaction>(sqlQuery, sqlParams);
        }

        public async Task<ForumReaction?> DeleteForumReactionAsync
        (
            int id,
            CancellationToken cancellationToken = default
        )
        {
            await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

            var sqlQuery = @"delete from forum_reactions where id = @Id
                returning id, user_id, message_id, reaction";
            
            var sqlParams = new
            {
                Id = id
            };

            return await connection.QuerySingleAsync<ForumReaction>(sqlQuery, sqlParams);
        }
    }
}
