using Dapper;
using Microsoft.Extensions.Options;
using WebRunApplication.Domain.Entities;
using WebRunApplication.Infrastructure.Interfaces;
using WebRunApplication.Infrastructure.Options;

namespace WebRunApplication.Infrastructure.Repositories
{
    internal class HelpMessageRepository(IOptions<PostgreOptions> options, ApplicationDbContext db)
        : DbRepository(options.Value),
            IHelpMessageRepository
    {
        public async Task<HelpMessage> CreateHelpMessageAsync
        (
            Models.HelpMessage helpMessage,
            CancellationToken cancellationToken = default
        )
        {
            ArgumentNullException.ThrowIfNull(helpMessage);
            
            await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

            var sqlQuery = @$"insert into help_messages(
                          (user_id,
                          date,
                          question,
                          answer)
    values (@{nameof(helpMessage.UserId)},
           @{nameof(helpMessage.Date)},
           @{nameof(helpMessage.Question)},
           @{nameof(helpMessage.Answer)}))
    returning id, user_id, date, question, answer";

            return await connection.QueryFirstAsync<HelpMessage>(sqlQuery);
        }

        public async Task<HelpMessage?> GetHelpMessageByIdAsync
        (
            int id, 
            CancellationToken cancellationToken = default
        )
        {
            await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

            var sqlQuery = "select * from help_messages where id = @Id";
            var sqlParams = new
            {
                Id = id
            };

            return await connection.QueryFirstOrDefaultAsync<HelpMessage>(sqlQuery, sqlParams);
        }

        public async Task<HelpMessage?> GetHelpMessageByUserIdAsync
        (
            int userId,
            CancellationToken cancellationToken = default
        )
        {
            await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

            var sqlQuery = "select * from help_messages where user_id = @UserId";
            var sqlParams = new
            {
                UserId = userId
            };

            return await connection.QueryFirstOrDefaultAsync<HelpMessage>(sqlQuery, sqlParams);
        }

        public async Task<IEnumerable<HelpMessage>> GetHelpMessagesAsync(CancellationToken cancellationToken = default)
        {
            await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

            var sqlQuery = "select * from help_messages";

            return await connection.QueryAsync<HelpMessage>(sqlQuery);
        }

        public async Task<HelpMessage> UpdateHelpMessageAsync
        (
            int id, 
            Models.HelpMessage helpMessage, 
            CancellationToken cancellationToken = default
        )
        {
            ArgumentNullException.ThrowIfNull(helpMessage);
            
            await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

            var sqlQuery = @"update help_messages
                set user_id = @UserId,
                    date = @Date,
                    question = @Question,
                    answer = @Answer
                 where id = @Id
                 returning id, user_id, date, question, answer";
            
            var sqlParams = new
            {
                Id = id,
                UserId = helpMessage.UserId,
                Question = helpMessage.Question,
                Answer = helpMessage.Answer
            };

            return await connection.QuerySingleAsync<HelpMessage>(sqlQuery, sqlParams);
        }

        public async Task<HelpMessage?> DeleteHelpMessage(int id, CancellationToken cancellationToken = default)
        {
            await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

            var sqlQuery = @"delete from help_messages where id = @Id
                returning id, user_id, date, question, answer";
            
            var sqlParams = new
            {
                Id = id
            };

            return await connection.QueryFirstOrDefaultAsync<HelpMessage>(sqlQuery, sqlParams);
        }
    }
}