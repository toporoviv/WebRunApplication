using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebRunApplication.Domain.Entities;
using WebRunApplication.Infrastructure.Interfaces;
using WebRunApplication.Services.Interfaces;

namespace WebRunApplication.Services.Implementations;

public class ChartService(
    ILogger<ChartService> logger,
    IUserRepository userRepository,
    IBaseRepository<Training> trainingRepository,
    IIndicatorRepository indicatorRepository,
    IBaseRepository<TrainingTemplate> trainingTemplateRepository)
    : IChartService
{
    // todo: логгер не используется((
    private readonly ILogger<ChartService> _logger = logger;

    public async Task<IBaseResponse<Dictionary<string, int>>> GetTrainingCountAsync
    (
        string login,
        CancellationToken cancellationToken
    )
    {
        var user = (await userRepository.GetUsersAsync(cancellationToken))
            .FirstOrDefault(x => x.Login == login);

        // todo: нужно обнюхать этот код, может его можно упростить
        var trainings = 
            (await indicatorRepository.GetIndicatorsAsync(cancellationToken))
                .Where(ind => ind.UserId == user.Id)
                .Join(
                    trainingRepository.GetAll(),
                    indicator => indicator.Date,
                    training => training.Date,
                    (indicator, training) => new
                    {
                        Indicator = indicator,
                        Training = training
                    })
                .Join(trainingTemplateRepository.GetAll(),
                    selector => selector.Training.TrainTemplateId,
                    template => template.Id,
                    (selector, template) => template.Title)
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());

        return new BaseResponse<Dictionary<string, int>>
        {
            Data = trainings,
            StatusCode = Domain.Enums.StatusCode.OK
        };
    }
}