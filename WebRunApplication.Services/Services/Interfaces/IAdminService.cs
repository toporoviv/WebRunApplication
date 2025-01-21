using WebRunApplication.Services.Models;

namespace WebRunApplication.Services.Interfaces
{
    public interface IAdminService
    {
        Task<IBaseResponse<List<HelpMessageViewModel>>> GetQuestionsAsync(CancellationToken cancellationToken = default);

        Task<IBaseResponse<bool>> CreateAnswerAsync(uint id, string answer, CancellationToken cancellationToken = default);

        Task<IBaseResponse<Dictionary<string, (int, int, int)>>> GetTopicsInformationAsync
        (
            CancellationToken cancellationToken = default
        );
    }
}
