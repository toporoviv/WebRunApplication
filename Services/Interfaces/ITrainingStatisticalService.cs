using WebRunApplication.Interfaces;
using WebRunApplication.Models;

namespace WebRunApplication.Services.Interfaces
{
    public interface ITrainingStatisticalService
    {
        Task<IBaseResponse<List<TrainingStatisticalTotalDurationView>>> GetTotalTrainingDuration(string login);

        Task<IBaseResponse<List<TrainingStatisticalCountViewModel>>> GetTotalTrainingCount(string login);

        Task<IBaseResponse<List<TrainingStatisticalTotalDurationView>>> GetTotalTrainingDayDuration(string login);

        Task<IBaseResponse<List<TrainingStatisticalMailingCount>>> GetTotalMailingCount(string login);

        Task<IBaseResponse<List<double>>> GetTotalTrainingDurationGroupByYearAndMonth(string login);

        Task<IBaseResponse<List<double>>> GetTotalTrainingCountGroupByYearAndMonth(string login);

        Task<IBaseResponse<List<double>>> GetTotalMailingCountGroupByYearAndMonth(string login);
    }
}
