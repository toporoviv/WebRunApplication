using WebRunApplication.Domain.Enums.Interfaces;

namespace WebRunApplication.Domain.Enums.Services.Interfaces
{
    public interface IChartService
    {
        Task<IBaseResponse<Dictionary<string, int>>> GetTrainingCount(string login);
    }
}
