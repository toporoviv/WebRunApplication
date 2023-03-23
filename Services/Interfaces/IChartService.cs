using WebRunApplication.Interfaces;

namespace WebRunApplication.Services.Interfaces
{
    public interface IChartService
    {
        Task<IBaseResponse<Dictionary<string, int>>> GetTrainingCount(string login);
    }
}
