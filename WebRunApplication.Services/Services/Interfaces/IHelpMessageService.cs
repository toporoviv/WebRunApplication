using WebRunApplication.Domain.Entities;

namespace WebRunApplication.Services.Interfaces;

public interface IHelpMessageService
{
    Task<IBaseResponse<HelpMessage>> CreateAsync
    (
        Infrastructure.Models.HelpMessage model,
        CancellationToken cancellationToken = default
    );
    Task<IBaseResponse<IEnumerable<HelpMessage>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IBaseResponse<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}