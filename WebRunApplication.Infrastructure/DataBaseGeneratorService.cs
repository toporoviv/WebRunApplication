using WebRunApplication.Domain.Entities;
using WebRunApplication.Infrastructure.Interfaces;

namespace WebRunApplication.Infrastructure;

public class DataBaseGeneratorService
(
    IUserRepository userRepository,
    IIndicatorRepository indicatorRepository,
    IBaseRepository<Training> trainingRepository,
    IBaseRepository<TrainingTemplate> trainingTemplateRepository
) : IDataBaseGenerator
{
    public async Task GenerateTrainings()
    {
        try
        {
            var usersCount = (await userRepository.GetUsersAsync(default)).Count();
            var trainingTemplateCount = trainingTemplateRepository.GetAll().Count();

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

                await indicatorRepository.CreateIndicatorAsync(new Models.Indicator
                {
                    Calories = indicator.Calories,
                    Date = indicator.Date,
                    Duration = indicator.Duration,
                    Steps = indicator.Steps,
                    AveragePulse = indicator.AveragePulse,
                    AverageSpeed = indicator.AverageSpeed,
                    MaximumPulse = indicator.MaximumPulse,
                    MinimumPulse = indicator.MinimumPulse,
                    UserId = indicator.UserId,
                    DiastolicPressure = indicator.DiastolicPressure,
                    SystolicPressure = indicator.SystolicPressure
                });
                await trainingRepository.Create(training);
            }
        }
        catch (Exception exception)
        {
            // todo: исправить exception
            throw new Exception();
        }
    }
}