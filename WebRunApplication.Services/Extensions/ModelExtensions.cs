using WebRunApplication.Domain.Enums;
using WebRunApplication.Infrastructure.Models;
using WebRunApplication.Services.Models;

namespace WebRunApplication.Services.Extensions;

internal static class ModelExtensions
{
    public static User ToUserWithoutId(this RegisterModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return new User
        {
            Age = model.Age,
            Email = model.Email,
            Fullname = model.Fullname,
            Gender = model.Gender,
            Height = model.Height,
            Login = model.Login,
            Password = model.Password,
            Weight = model.Weight,
            Role = Role.User
        };
    }

    public static HelpMessage ToHelpMessageWithoutId(this Domain.Entities.HelpMessage helpMessage)
    {
        ArgumentNullException.ThrowIfNull(helpMessage);
        
        return new HelpMessage
        {
            Question = helpMessage.Question,
            Date = helpMessage.Date,
            UserId = helpMessage.UserId,
            Answer = helpMessage.Answer
        };
    }

    public static Indicator ToIndicatorWithoutId(this Domain.Entities.Indicator indicator)
    {
        return new Indicator
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
        };
    }
}