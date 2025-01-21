using WebRunApplication.Domain.Entities;

namespace WebRunApplication.Infrastructure.Interfaces;

public interface IHelpMessageRepository
{
    Task<HelpMessage> CreateHelpMessageAsync
    (
        Models.HelpMessage helpMessage,
        CancellationToken cancellationToken = default
    );
    Task<HelpMessage?> GetHelpMessageByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<HelpMessage?> GetHelpMessageByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<HelpMessage>> GetHelpMessagesAsync(CancellationToken cancellationToken = default);
    Task<HelpMessage> UpdateHelpMessageAsync
    (
        int id, 
        Models.HelpMessage helpMessage,
        CancellationToken cancellationToken = default
    );

    Task<HelpMessage?> DeleteHelpMessage(int id, CancellationToken cancellationToken = default);

}