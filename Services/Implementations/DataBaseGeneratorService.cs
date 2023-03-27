using WebRunApplication.DAL.Interfaces;
using WebRunApplication.DataEntity;
using WebRunApplication.DataEntity.Forum;
using WebRunApplication.Enums;
using WebRunApplication.Interfaces;
using WebRunApplication.Response;
using WebRunApplication.Services.Interfaces;

namespace WebRunApplication.Services.Implementations
{
    public class DataBaseGeneratorService : IDataBaseGeneratorService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Indicator> _indicatorRepository;
        private readonly IBaseRepository<Training> _trainingRepository;
        private readonly IBaseRepository<TrainingTemplate> _trainingTemplateRepository;
        private readonly ILogger<DataBaseGeneratorService> _logger;

        public DataBaseGeneratorService(
            IBaseRepository<User> userRepository,
            IBaseRepository<Indicator> indicatorRepository,
            IBaseRepository<Training> trainingRepository,
            IBaseRepository<TrainingTemplate> trainingTemplateRepository,
            ILogger<DataBaseGeneratorService> logger)
        {
            _userRepository = userRepository;
            _indicatorRepository = indicatorRepository;
            _trainingRepository = trainingRepository;
            _trainingTemplateRepository = trainingTemplateRepository;
            _logger = logger;
        }

        public async Task<IBaseResponse<bool>> GenerateTrainings()
        {
            try
            {
                var usersCount = _userRepository.GetAll().Count();
                var trainingTemplateCount = _trainingTemplateRepository.GetAll().Count();

                var random = new Random();

                for (int i = 0; i < 200; i++)
                {
                    var indicator = new Indicator
                    {
                        UserId = (uint)random.Next(1, usersCount + 1),
                        Date = new DateTime(random.Next(2022, 2024), random.Next(1, 13), random.Next(1, 28)),
                        Pressure = null,
                        Calories = (uint)random.Next(200, 1001),
                        AverageSpeed = random.NextDouble() * 10,
                        MinimumPulse = (uint)random.Next(80, 131),
                        AveragePulse = (uint)random.Next(90, 171),
                        MaximumPulse = (uint)random.Next(120, 191),
                        Steps = (uint)random.Next(1000, 15001)
                    };

                    var training = new Training
                    {
                        Date = indicator.Date,
                        TrainTemplateId = (uint)random.Next(1, trainingTemplateCount + 1),
                        Duration = new TimeSpan(random.Next(0, 1), random.Next(1, 60), random.Next(1, 60))
                    };

                    await _indicatorRepository.Create(indicator);
                    await _trainingRepository.Create(training);
                }

                return new BaseResponse<bool>
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[{nameof(DataBaseGeneratorService)}.{nameof(GenerateTrainings)}] error: {exception.Message}");
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {exception.Message}"
                };
            }
        }
    }
}
