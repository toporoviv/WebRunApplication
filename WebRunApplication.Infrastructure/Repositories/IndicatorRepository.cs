using Dapper;
using Microsoft.Extensions.Options;
using WebRunApplication.Domain.Entities;
using WebRunApplication.Infrastructure.Interfaces;
using WebRunApplication.Infrastructure.Options;

namespace WebRunApplication.Infrastructure.Repositories;

internal class IndicatorRepository(IOptions<PostgreOptions> postgreOptions) 
    : DbRepository(postgreOptions.Value), IIndicatorRepository
{
    public async Task<Indicator> CreateIndicatorAsync
    (
        Models.Indicator indicator,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentNullException.ThrowIfNull(indicator);
        
        await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

        var sqlQuery = @$"insert into indicators(
                       user_id,
                       date,
                       systolic_pressure,
                       diastolic_pressure,
                       duration,
                       calories,
                       average_speed,
                       minimum_pulse,
                       average_pulse,
                       maximum_pulse,
                       steps)
                   values(
                       @{nameof(indicator.UserId)},
                          @{nameof(indicator.Date)},
                          @{nameof(indicator.SystolicPressure)},
                          @{nameof(indicator.DiastolicPressure)},
                          @{nameof(indicator.Duration)},
                          @{nameof(indicator.Calories)},
                          @{nameof(indicator.AverageSpeed)},
                          @{nameof(indicator.MinimumPulse)},
                          @{nameof(indicator.AveragePulse)},
                          @{nameof(indicator.MaximumPulse)},
                          @{nameof(indicator.Steps)},
                   )
                   returning id,
                       user_id,
                       date,
                       systolic_pressure,
                       diastolic_pressure,
                       duration,
                       calories,
                       average_speed,
                       minimum_pulse,
                       average_pulse,
                       maximum_pulse,
                       steps";

        return await connection.QueryFirstAsync<Indicator>(sqlQuery);
    }

    public async Task<Indicator?> GetIndicatorByIdAsync
    (
        int id,
        CancellationToken cancellationToken = default
    )
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id));
        
        await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

        var sqlQuery = "select * from indicators where id = @Id";

        var sqlParams = new
        {
            Id = id
        };
        
        return await connection.QueryFirstOrDefaultAsync<Indicator>(sqlQuery, sqlParams);
    }

    public async Task<IEnumerable<Indicator>> GetIndicatorsAsync(CancellationToken cancellationToken = default)
    {
        await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

        var sqlQuery = "select * from indicators";

        return await connection.QueryAsync<Indicator>(sqlQuery);
    }

    public async Task<IEnumerable<Indicator>> GetIndicatorsByUserIdAsync
    (
        int userId,
        CancellationToken cancellationToken = default
    )
    {
        if (userId <= 0)
            throw new ArgumentOutOfRangeException(nameof(userId));

        await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

        var sqlQuery = "select * from indicators where user_id = @UserId";
        var sqlParams = new
        {
            UserId = userId
        };

        return await connection.QueryAsync<Indicator>(sqlQuery, sqlParams);
    }

    public async Task<Indicator> UpdateIndicatorAsync
    (
        int id,
        Models.Indicator indicator,
        CancellationToken cancellationToken = default
    )
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id));
        
        ArgumentNullException.ThrowIfNull(indicator);

        await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

        var sqlQuery = @"update indicators
            set user_id = @UserId,
                date = @Date,
                systolic_pressure = @SystolicPressure,
                diastolic_pressure = @DiastolicPressure,
                duration = @Duration,
                calories = @Calories,
                average_speed = @AverageSpeed,
                minimum_pulse = @MinimumPulse,
                average_pulse = @AveragePulse,
                maximum_pulse = @MaximumPulse,
                steps = @Steps
            where id = @Id
            returning id,
                user_id,
                date,
                systolic_pressure,
                diastolic_pressure,
                duration,
                calories,
                average_speed,
                minimum_pulse,
                average_pulse,
                maximum_pulse,
                steps";

        var sqlParams = new
        {
            Id = id,
            UserId = indicator.UserId,
            Date = indicator.Date,
            SystolicPressure = indicator.SystolicPressure,
            Duration = indicator.Duration,
            Calories = indicator.Calories,
            AverageSpeed = indicator.AverageSpeed,
            MinimumPulse = indicator.MinimumPulse,
            AveragePulse = indicator.AveragePulse,
            MaximumPulse = indicator.MaximumPulse,
            Steps = indicator.Steps
        };

        return await connection.QueryFirstAsync<Indicator>(sqlQuery, sqlParams);
    }

    public async Task<Indicator?> DeleteIndicatorAsync
    (
        int id,
        CancellationToken cancellationToken = default
    )
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id));

        await using var connection = await GetAndOpenConnectionAsync(cancellationToken);

        var sqlQuery = @"delete from indicators where id = @Id
            returning id,
                   user_id,
                   date,
                   systolic_pressure,
                   diastolic_pressure,
                   duration,
                   calories,
                   average_speed,
                   minimum_pulse,
                   average_pulse,
                   maximum_pulse,
                   steps";

        var sqlParams = new
        {
            Id = id
        };
        
        return await connection.QueryFirstOrDefaultAsync<Indicator>(sqlQuery, sqlParams);
    }
}