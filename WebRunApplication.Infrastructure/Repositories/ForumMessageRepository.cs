using Dapper;
using Microsoft.Extensions.Options;
using WebRunApplication.Domain.Entities.Forum;
using WebRunApplication.Infrastructure.Interfaces;
using WebRunApplication.Infrastructure.Options;

namespace WebRunApplication.Infrastructure.Repositories
{
    internal class ForumMessageRepository(IOptions<PostgreOptions> options, ApplicationDbContext db)
        : DbRepository(options.Value),
            IForumMessageRepository
    {
        public async Task<ForumMessage> CreateForumMessageAsync
        (
            Models.ForumMessage forumMessage,
            CancellationToken cancellationToken = default
        )
        {
            ArgumentNullException.ThrowIfNull(forumMessage);
            
            await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

            var sqlQuery = @$"insert into forum_messages(
                           user_id,
                           parent_id,
                           date,
                           message)
                       values(
                            @{nameof(forumMessage.UserId)},
                            @{nameof(forumMessage.ParentId)},
                            @{nameof(forumMessage.Date)},
                            @{nameof(forumMessage.Message)}
                       )
                       returning id, user_id, parent_id, date, message";

            return await connection.QueryFirstAsync<ForumMessage>(sqlQuery);
        }

        public async Task<ForumMessage?> GetForumMessageByIdAsync
        (
            int id,
            CancellationToken cancellationToken = default
        )
        {
            await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

            var sqlQuery = "select * from forum_messages where id = @Id";
            var sqlParams = new
            {
                Id = id
            };

            return await connection.QueryFirstOrDefaultAsync<ForumMessage>(sqlQuery, sqlParams);
        }

        public async Task<IEnumerable<ForumMessage>> GetForumMessages(CancellationToken cancellationToken = default)
        {
            await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

            var sqlQuery = "select * from forum_messages";

            return await connection.QueryAsync<ForumMessage>(sqlQuery);
        }

        public async Task<ForumMessage> UpdateForumMessageAsync
        (
            int id,
            Models.ForumMessage forumMessage,
            CancellationToken cancellationToken = default
        )
        {
            ArgumentNullException.ThrowIfNull(forumMessage);

            await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

            var sqlQuery = @"update forum_messages
                set user_id = @UserId,
                    parent_id = @ParentId,
                    date = @Date,
                    message = @Message
                where id = @Id
                returning id, user_id, parent_id, date, message";

            return await connection.QuerySingleAsync<ForumMessage>(sqlQuery);
        }

        public async Task<ForumMessage?> DeleteForumMessageAsync
        (
            int id,
            CancellationToken cancellationToken = default
        )
        {
            await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

            var sqlQuery = @"delete from forum_messages where id = @Id
                returning id, user_id, parent_id, date, message";

            return await connection.QueryFirstOrDefaultAsync<ForumMessage>(sqlQuery);
        }
    }
}

