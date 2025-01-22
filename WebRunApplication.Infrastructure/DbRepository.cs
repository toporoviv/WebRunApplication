using System.Transactions;
using Npgsql;
using WebRunApplication.Infrastructure.Options;

namespace WebRunApplication.Infrastructure;

internal abstract class DbRepository(PostgreOptions postgreOptions)
{
    public TransactionScope CreateTransactionScope
    (
        IsolationLevel level = IsolationLevel.ReadCommitted
    )
    {
        return new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions
            {
                IsolationLevel = level,
                Timeout = TimeSpan.FromSeconds(5)
            },
            TransactionScopeAsyncFlowOption.Enabled);
    }

    protected async Task<NpgsqlConnection> GetAndOpenConnectionAsync(CancellationToken cancellationToken)
    {
        var connection = new NpgsqlConnection(postgreOptions.ConnectionString);
        await connection.OpenAsync(cancellationToken);
        await connection.ReloadTypesAsync(cancellationToken);
        return connection;
    }
}