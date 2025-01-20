using WebRunApplication.Services.Models;

namespace WebRunApplication.Services.Services.Interfaces
{
    
    // todo: понюхать методы, думаю можно что то исправить
    public interface ITrainingStatisticalService
    {
        Task<IBaseResponse<List<TrainingStatisticalTotalDurationView>>> GetTotalTrainingDurationAsync
        (
            string login,
            CancellationToken cancellationToken = default
        );
        Task<IBaseResponse<List<TrainingStatisticalCountViewModel>>> GetTotalTrainingCountAsync
        (
            string login,
            CancellationToken cancellationToken = default
        );
        Task<IBaseResponse<List<TrainingStatisticalTotalDurationView>>> GetTotalTrainingDayDurationAsync
        (
            string login,
            CancellationToken cancellationToken = default
        );
        Task<IBaseResponse<List<TrainingStatisticalMailingCount>>> GetTotalMailingCountAsync
        (
            string login,
            CancellationToken cancellationToken = default
        );
        Task<IBaseResponse<List<double>>> GetTotalTrainingDurationGroupByYearAndMonthAsync
        (
            string login,
            CancellationToken cancellationToken = default
        );
        Task<IBaseResponse<List<double>>> GetTotalTrainingCountGroupByYearAndMonthAsync
        (
            string login,
            CancellationToken cancellationToken = default
        );
        Task<IBaseResponse<List<double>>> GetTotalMailingCountGroupByYearAndMonthAsync
        (
            string login,
            CancellationToken cancellationToken = default
        );
    }
}
