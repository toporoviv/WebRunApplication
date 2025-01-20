using WebRunApplication.Domain.Entities;
using WebRunApplication.Infrastructure.Interfaces;

namespace WebRunApplication.Infrastructure
{
    public class DataBaseGeneratorService : IDataBaseGenerator
    {
        private readonly IUserRepository _userRepository;
        private readonly IBaseRepository<Indicator> _indicatorRepository;
        private readonly IBaseRepository<Training> _trainingRepository;
        private readonly IBaseRepository<TrainingTemplate> _trainingTemplateRepository;

        public DataBaseGeneratorService(
            IUserRepository userRepository,
            IBaseRepository<Indicator> indicatorRepository,
            IBaseRepository<Training> trainingRepository,
            IBaseRepository<TrainingTemplate> trainingTemplateRepository)
        {
            _userRepository = userRepository;
            _indicatorRepository = indicatorRepository;
            _trainingRepository = trainingRepository;
            _trainingTemplateRepository = trainingTemplateRepository;
        }

        public async Task GenerateTrainings()
        {
            try
            {
                var usersCount = (await _userRepository.GetUsersAsync(default)).Count();
                var trainingTemplateCount = _trainingTemplateRepository.GetAll().Count();

                var random = new Random();

                for (var i = 0; i < 200; i++)
                {
                    var indicator = new Indicator
                    {
                        UserId = random.Next(1, usersCount + 1),
                        Date = new DateTime(random.Next(2022, 2024), random.Next(1, 13), random.Next(1, 28)),
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
                        TrainTemplateId = random.Next(1, trainingTemplateCount + 1),
                        Duration = new TimeSpan(random.Next(0, 1), random.Next(1, 60), random.Next(1, 60))
                    };

                    await _indicatorRepository.Create(indicator);
                    await _trainingRepository.Create(training);
                }
            }
            catch (Exception exception)
            {
                // todo: исправить exception
                throw new Exception();
            }
        }
    }
}
