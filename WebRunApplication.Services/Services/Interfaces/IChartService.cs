namespace WebRunApplication.Services.Services.Interfaces
{
    public interface IChartService
    {
        Task<IBaseResponse<Dictionary<string, int>>> GetTrainingCountAsync
        (
            string login,
            CancellationToken cancellationToken = default
        );
    }
}
