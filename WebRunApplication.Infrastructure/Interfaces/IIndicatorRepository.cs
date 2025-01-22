using WebRunApplication.Domain.Entities;

namespace WebRunApplication.Infrastructure.Interfaces;

public interface IIndicatorRepository
{
    Task<Indicator> CreateIndicatorAsync(Models.Indicator indicator, CancellationToken cancellationToken = default);
    Task<Indicator?> GetIndicatorByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Indicator>> GetIndicatorsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Indicator>> GetIndicatorsByUserIdAsync(int userId, CancellationToken cancellationToken = default);

    Task<Indicator> UpdateIndicatorAsync
    (
        int id, 
        Models.Indicator indicator,
        CancellationToken cancellationToken = default
    );

    Task<Indicator?> DeleteIndicatorAsync(int id, CancellationToken cancellationToken = default);
}