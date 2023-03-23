using Microsoft.EntityFrameworkCore;
using WebRunApplication.DAL.Interfaces;
using WebRunApplication.DAL.Repositories;
using WebRunApplication.DataEntity;
using WebRunApplication.Interfaces;
using WebRunApplication.Response;
using WebRunApplication.Services.Interfaces;

namespace WebRunApplication.Services.Implementations
{
    public class ChartService : IChartService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Training> _trainingRepository;
        private readonly IBaseRepository<Indicator> _indicatorRepository;
        private readonly IBaseRepository<TrainingTemplate> _trainingTemplateRepository;
        private readonly ILogger<ChartService> _logger;

        public ChartService(
            ILogger<ChartService> logger,
            IBaseRepository<User> userRepository,
            IBaseRepository<Training> trainingRepository,
            IBaseRepository<Indicator> indicatorRepository,
            IBaseRepository<TrainingTemplate> trainingTemplateRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _trainingRepository = trainingRepository;
            _indicatorRepository = indicatorRepository;
            _trainingTemplateRepository = trainingTemplateRepository;
        }

        public async Task<IBaseResponse<Dictionary<string, int>>> GetTrainingCount(string login)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Login == login);

            var trainings = 
                _indicatorRepository
                .GetAll()
                .Where(ind => ind.UserId == user.Id)
                .Join(_trainingRepository.GetAll(), ind => ind.Date, train => train.Date, (ind, train) => new { ind, train })
                .Join(_trainingTemplateRepository.GetAll(), selector => selector.train.TrainTemplateId,
                    template => template.Id, (selector, template) => template.Title)
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());

            return new BaseResponse<Dictionary<string, int>>
            {
                Data = trainings,
                StatusCode = Enums.StatusCode.OK
            };
        }
    }
}
