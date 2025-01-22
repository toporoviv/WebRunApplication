using WebRunApplication.Domain.Entities;

namespace WebRunApplication.Services.Interfaces;

public interface IIndicatorService
{
    Task<IBaseResponse<Indicator>> CreateAsync
    (
        Infrastructure.Models.Indicator model,
        CancellationToken cancellationToken = default
    );

    Task<IBaseResponse<IEnumerable<Indicator>>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IBaseResponse<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}