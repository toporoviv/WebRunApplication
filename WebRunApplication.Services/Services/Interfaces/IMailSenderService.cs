namespace WebRunApplication.Services.Interfaces;

public interface IMailSenderService
{
    // todo: исправить uint на int
    Task<IBaseResponse<bool>> SendMessageAsync
    (
        uint userId,
        string emailTo,
        string message,
        string topic,
        CancellationToken cancellationToken = default
    );
}